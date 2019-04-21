export interface ConsentDto
{
    ClientName: string;
    ClientUrl: string;
    ClientLogoUrl: string;
    AllowRememberConsent: boolean;

    IdentityScopes: ScopeDto[];
    ResourceScopes: ScopeDto[];
    Button: string;
    ScopesConsented: string[];
    RememberConsent: boolean;
    ReturnUrl: string;
}

export interface ScopeDto
{
    Name: string;
    DisplayName: string;
    Description: string;
    Emphasize: boolean;
    Required: boolean;
    Checked: boolean;
}