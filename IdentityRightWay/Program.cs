﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IdentityRightWay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseMetricsWebTracking()
                //.UseMetricsEndpoints()
                .ConfigureMetricsWithDefaults(
                builder =>
                {
                    builder.Report.ToInfluxDb("http://localhost:8086", "metricsdatabase", TimeSpan.FromSeconds(1)).Build();
                })
                .UseMetrics()
                .UseStartup<Startup>();
    }
}
