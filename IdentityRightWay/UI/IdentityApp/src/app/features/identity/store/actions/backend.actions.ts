import { Action } from '@ngrx/store';
import { LoginDto } from 'src/app/models/LoginDto';
import { LoginCommand } from 'src/app/models/LoginCommand';
import { RegisterCommand } from 'src/app/models/RegisterCommand';
import { IdentityRightWayResponseBase } from 'src/app/models/IdentityRightWayResponseBase';


export enum IdentityBackendActionsTypes
{
    FetchUserLogin = "[api/identity/login] fetch user login",
    FetchUserLoginSuccess = "[api/identity/login] fetch user login success",
    FetchUserLoginFail = "[api/identity/login] fetch user login fail",
    FetchUserRegister = "[api/identity/register] fetch user register",
    FetchUserRegisterSuccess = "[api/identity/register] fetch user register success",
    FetchUserRegisterFail = "[api/identity/register] fetch user register fail",
}

export class FetchUserLogin implements Action
{
    readonly type = IdentityBackendActionsTypes.FetchUserLogin;
    constructor(public payload : LoginCommand){}
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

export type IdentityBackendActions = 
FetchUserLogin |
FetchUserLoginSuccess |
FetchUserLoginFail |
FetchUserRegister |
FetchUserRegisterFail |
FetchUserRegisterSuccess
