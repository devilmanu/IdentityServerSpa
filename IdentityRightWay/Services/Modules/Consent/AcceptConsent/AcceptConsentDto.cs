using IdentityRightWay.Infrastructure.TypeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Services.Modules.Consent.AcceptConsent
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    public class AcceptConsentDto
    {
        public string RedirectUri { get; set; }
    }

}
