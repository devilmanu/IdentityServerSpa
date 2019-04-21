import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';
import { LoginStore, loginReducer } from './reducers/login.reducers';
import { RegisterStore, registerReducer } from './reducers/register.reducer';
import { ConsentStore, consentReducer } from './reducers/consent.reducer';


export interface IdentityState{
    login : LoginStore
    register  : RegisterStore
    consent : ConsentStore
}


export const reducers : ActionReducerMap<IdentityState> = {
    register : registerReducer,
    login : loginReducer,
    consent : consentReducer
}
