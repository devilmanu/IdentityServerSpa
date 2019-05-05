/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ConsentInputDto } from "../models/consentInputDto";
import { ScopeDto } from "../models/scopeDto";

export interface ConsentDto extends ConsentInputDto {
    clientName: string;
    clientUrl: string;
    clientLogoUrl: string;
    allowRememberConsent: boolean;
    identityScopes: ScopeDto[];
    resourceScopes: ScopeDto[];
}
