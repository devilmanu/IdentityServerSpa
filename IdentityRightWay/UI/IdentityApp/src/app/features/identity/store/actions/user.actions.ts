import { Action } from '@ngrx/store';

export enum UserActionsTypes
{
    UserLogin = "[USER/LOGIN] user login"
}

export class UserLogin implements Action
{
    readonly type: string;
    constructor(public payload : any){}
}


export type UserActions = UserLogin