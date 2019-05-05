using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Infrastructure.TypeGen;
using IdentityRightWay.Services.Modules.Consent.GetConsent;
using IdentityRightWay.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace IdentityRightWay.Services.Modules.Consent.AcceptConsent
{
    [ExportTsInterface(OutputDir = ExportPathTsInterfaces.BasePath)]
    [TsIgnoreBase]
    public class AcceptConsentCommand : ConsentInputDto, IQuery<IdentityRightWayResponseBase<AcceptConsentDto>>
    {

    }


}
