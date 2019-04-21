using IdentityRightWay.Infrastructure.Bus.Commands;
using IdentityRightWay.Infrastructure.Bus.Queries;
using IdentityRightWay.Services.Modules.Consent.GetConsent;
using IdentityRightWay.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRightWay.Services.Modules.Consent.AcceptConsent
{
    public class AcceptConsentCommand : ConsentInputDto, IQuery<IdentityRightWayResponseBase<AcceptConsentDto>>
    {

    }


}
