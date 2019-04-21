
import { UserActions } from '../actions/user.actions';
import { IdentityBackendActions, IdentityBackendActionsTypes } from '../actions/backend.actions';
import { ConsentDto } from 'src/app/models/ConsentDto';


export interface ConsentStore {
    consent? : ConsentDto
    loading? : boolean
    errors? : string[]
}
const initialState: ConsentStore = {}


export function consentReducer(
    state: ConsentStore = initialState,
    action: IdentityBackendActions | UserActions
) : ConsentStore {
    switch (action.type) {
        case IdentityBackendActionsTypes.FetchConsent:
            return { ...state, loading: true }
        case IdentityBackendActionsTypes.FetchConsentFail:
            return { ...state, loading: false, errors : action.payload  }
        case IdentityBackendActionsTypes.FetchConsentSuccess:
            return { ...state, loading: false, consent : action.payload }    
        default:
            return state;
    }
}
