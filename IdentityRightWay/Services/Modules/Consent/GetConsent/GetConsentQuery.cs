using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Consent.GetConsent
{
    public class GetConsentQuery : IQuery<IdentityRightWayResponseBase<ConsentDto>>
    {
        public string ReturnUrl { get; set; }
    }
}
