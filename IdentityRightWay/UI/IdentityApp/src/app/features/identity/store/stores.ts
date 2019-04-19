import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';
import { LoginStore, loginReducer } from './reducers/login.reducers';
import { RegisterStore, registerReducer } from './reducers/register.reducer';


export interface IdentityState{
    login : LoginStore
    register  : RegisterStore
}


export const reducers : ActionReducerMap<IdentityState> = {
    register : registerReducer,
    login : loginReducer
}
