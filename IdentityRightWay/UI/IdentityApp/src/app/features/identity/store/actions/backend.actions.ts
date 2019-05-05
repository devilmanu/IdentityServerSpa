import { Action } from '@ngrx/store';
import { LoginDto } from 'src/app/models/LoginDto';
import { RegisterCommand } from 'src/app/models/RegisterCommand';
import { IdentityRightWayResponseBase } from 'src/app/models/IdentityRightWayResponseBase';
import { GetConsentQuery } from 'src/app/models/GetConsentQuery';
import { ConsentDto } from 'src/app/models/ConsentDto';
import { AcceptConsentDto } from 'src/app/models/AcceptConsentDto';
import { AcceptConsentCommand } from 'src/app/models/AcceptConsentCommand';
import { LoginQuery } from 'src/app/models/loginQuery';


export enum IdentityBackendActionsTypes
{
    FetchUserLogin = "[api/identity/login] fetch user login",
    FetchUserLoginSuccess = "[api/identity/login] fetch user login success",
    FetchUserLoginFail = "[api/identity/login] fetch user login fail",
    FetchUserRegister = "[api/identity/register] fetch user register",
    FetchUserRegisterSuccess = "[api/identity/register] fetch user register success",
    FetchUserRegisterFail = "[api/identity/register] fetch user register fail",
    FetchConsent = "[api/consent/get] fetch user register",
    FetchConsentSuccess = "[api/consent/get] fetch user register success",
    FetchConsentFail = "[api/consent/get] fetch user register fail",
    FetchAcceptConsent = "[api/consent/accept] fetch user register",
    FetchAcceptConsentSuccess = "[api/consent/accept] fetch user register success",
    FetchAcceptConsentFail = "[api/consent/accept] fetch user register fail",
}

export class FetchUserLogin implements Action
{
    readonly type = IdentityBackendActionsTypes.FetchUserLogin;
    constructor(public payload : LoginQuery){}
}

export class FetchUserLoginSuccess implements Action
{
    readonly type = IdentityBackendActionsTypes.FetchUserLoginSuccess;
    constructor(public payload : IdentityRightWayResponseBase<LoginDto>){}
}

export class FetchUserLoginFail implements Action
{
    readonly type =  IdentityBackendActionsTypes.FetchUserLoginFail;
    constructor(public payload : string[]){}
}

export class FetchUserRegister implements Action
{
    readonly type = IdentityBackendActionsTypes.FetchUserRegister;
    constructor(public payload : RegisterCommand){}
}

export class FetchUserRegisterSuccess implements Action
{
    readonly type = IdentityBackendActionsTypes.FetchUserRegisterSuccess;
    constructor(public payload? : void){}
}

export class FetchUserRegisterFail implements Action
{
    readonly type =  IdentityBackendActionsTypes.FetchUserRegisterFail;
    constructor(public payload : string[]){}
}

export class FetchConsent implements Action
{
    readonly type =  IdentityBackendActionsTypes.FetchConsent;
    constructor(public payload : GetConsentQuery){}
}

export class FetchConsentFail implements Action
{
    readonly type =  IdentityBackendActionsTypes.FetchUserLoginFail;
    constructor(public payload : string[]){}
}

export class FetchConsentSuccess implements Action
{
    readonly type =  IdentityBackendActionsTypes.FetchConsentSuccess;
    constructor(public payload : IdentityRightWayResponseBase<ConsentDto>){}
}

export class FetchAcceptConsent implements Action
{
    readonly type =  IdentityBackendActionsTypes.FetchAcceptConsent;
    constructor(public payload : AcceptConsentCommand){}
}

export class FetchAcceptConsentSuccess implements Action
{ 
    readonly type =  IdentityBackendActionsTypes.FetchAcceptConsentSuccess;
    constructor(public payload : IdentityRightWayResponseBase<AcceptConsentDto>){}
}

export class FetchAcceptConsentFail implements Action
{
    readonly type =  IdentityBackendActionsTypes.FetchAcceptConsentFail;
    constructor(public payload : string[]){}
}


export type IdentityBackendActions = 
FetchUserLogin |
FetchUserLoginSuccess |
FetchUserLoginFail |
FetchUserRegister |
FetchUserRegisterFail |
FetchUserRegisterSuccess |
FetchConsent |
FetchConsentSuccess |
FetchConsentFail | 
FetchAcceptConsent |
FetchAcceptConsentSuccess |
FetchAcceptConsentFail
