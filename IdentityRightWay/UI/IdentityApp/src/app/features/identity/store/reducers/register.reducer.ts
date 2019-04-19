
import { UserActions } from '../actions/user.actions';
import { IdentityBackendActions, IdentityBackendActionsTypes } from '../actions/backend.actions';


export interface RegisterStore {
    loading? : boolean
    errors? : string[]
}
const initialState: RegisterStore = {}


export function registerReducer(
    state: RegisterStore = initialState,
    action: IdentityBackendActions | UserActions
) : RegisterStore {
    switch (action.type) {
        case IdentityBackendActionsTypes.FetchUserRegister:
            return { ...state, loading: true }
        case IdentityBackendActionsTypes.FetchUserRegisterFail:
            return { ...state, loading: false, errors : action.payload  }
        case IdentityBackendActionsTypes.FetchUserRegisterSuccess:
            return { ...state, loading: false }    
        default:
            return state;
    }
}
