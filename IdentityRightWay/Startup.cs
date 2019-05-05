using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using App.Metrics.AspNetCore.Tracking;
using FluentValidation.AspNetCore;
using IdentityRightWay.Api.Shared;
using IdentityRightWay.Domain.Entities;
using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Infrastructure.EntityFramework;
using IdentityRightWay.Infrastructure.IdentityServer4.Configuration;
using IdentityRightWay.Services.Modules.Profile;
using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace IdentityRightWay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<ServiceFactory>(p => p.GetService);

            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();

            var migrationAssemby = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //var metrics = new MetricsBuilder()
            //    .Report.ToInfluxDb("http://influxdb:8086", "metricsdatabase")
            //    .Build();

            //services.AddMetrics(metrics);
            //services.AddSingleton<IStartupFilter>(new DefaultMetricsTrackingStartupFilter());

            services.AddMvc(opts =>
            {
                opts.Filters.Add(new IdentityRightWayExceptionHandler());
                opts.Filters.Add(new IdentityRightWayValidationHandler());
                opts.Filters.Add(new MetricsResourceFilter(new MvcRouteTemplateResolver()));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1) //TODO: 2.1 por la mierda de appmetrics
                .AddFluentValidation() //añadimos flient validatos
                .ConfigureApiBehaviorOptions(opts =>
                {
                    opts.SuppressModelStateInvalidFilter = true;
                })
                .AddMetrics();
            //services.AddMetrics(opts =>
            //{
            //    opts.Report.("http://localhost:9200", "metricsindex");
            //})

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Startup))
                .AddClasses()
                .AsImplementedInterfaces());

            services.AddDbContext<AppIdentityDbContext>(opts =>
            {
                //dotnet ef migrations add initial
                opts.UseSqlServer(@"Server=localhost; Database=IdentityRightWay; User Id=sa; password=StrongPassw0rd; Trusted_Connection=False; MultipleActiveResultSets = true;",
                    sql => {
                        sql.MigrationsAssembly(migrationAssemby);
                    });
            });

            services.AddIdentity<AppUser, AppRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            //https://localhost:5001/.well-known/openid-configuration
            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    //options.UserInteraction.LoginUrl = "/login";
                    //options.UserInteraction.LoginReturnUrlParameter = "/kk";
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Resources.IdentityResources)
                .AddInMemoryApiResources(Resources.ApiResources)
                .AddInMemoryClients(Clients.Get())
                .AddAspNetIdentity<AppUser>()
                .AddProfileService<ProfileService>();

            //services.AddAuthentication("cookies")
            //    .AddCookie("cookie", opts => opts.)

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Identity API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            //swagger
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "UI/IdentityApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseIdentityServer();

            app.UseMvc();


            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "UI/IdentityApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "ng serve");
                }
            });
        }
    }
}
