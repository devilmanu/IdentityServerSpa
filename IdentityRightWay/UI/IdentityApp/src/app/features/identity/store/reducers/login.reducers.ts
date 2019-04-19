
import { UserActions } from '../actions/user.actions';
import { IdentityBackendActions, IdentityBackendActionsTypes } from '../actions/backend.actions';

export interface LoginStore {
    loading? : boolean
    errors? : string[]
}
const initialState: LoginStore = {}


export function loginReducer(
    state: LoginStore = initialState,
    action: IdentityBackendActions | UserActions
) : LoginStore {
    switch (action.type) {
        case IdentityBackendActionsTypes.FetchUserLogin:
            return { ...state, loading: true }
        case IdentityBackendActionsTypes.FetchUserLoginFail:
            return { ...state, loading: false, errors : action.payload }
        case IdentityBackendActionsTypes.FetchUserLoginSuccess:
            return { ...state, loading: false }    
        default:
            return state;
    }
}
