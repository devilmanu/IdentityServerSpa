import { ConsentDto } from './ConsentDto';

export interface AcceptConsentCommand
{
    Button: string;
    ScopesConsented: string[];
    RememberConsent: boolean;
    ReturnUrl: string;
}