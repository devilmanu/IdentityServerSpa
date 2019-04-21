using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Infrastructure.IdentityServer4.Configuration
{
    public static class Resources
    {
        public static readonly List<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static readonly List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource("api1", "My API #1")
        };
    }
}
