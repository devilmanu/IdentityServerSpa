import { Injectable } from '@angular/core';
import { FetchUserLogin, FetchUserLoginSuccess, FetchUserLoginFail, IdentityBackendActionsTypes, FetchUserRegister, FetchUserRegisterFail, FetchUserRegisterSuccess } from '../actions/backend.actions';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { IdentityService } from '../../services/identity.service';
import { switchMap, catchError, mergeMap, tap } from 'rxjs/operators';
import { of, throwError } from 'rxjs';

@Injectable()
export class BackendEffects
{
    constructor(private actions$: Actions, private identityApiService : IdentityService)
    {

    }
    
    @Effect()
    FetchUserLogin$ = this.actions$.pipe(
        ofType<FetchUserLogin>(IdentityBackendActionsTypes.FetchUserLogin),
        switchMap((action) => this.identityApiService.login(action.payload.email, action.payload.password).pipe(
            mergeMap(o => o.isValid ? of( new FetchUserLoginSuccess(o)) : throwError(o.errors)),
            catchError((err) => of(new FetchUserLoginFail(err)))
        ))
    )

    @Effect()
    FetchUserRegister$ = this.actions$.pipe(
        ofType<FetchUserRegister>(IdentityBackendActionsTypes.FetchUserRegister),
        switchMap((action) => this.identityApiService.register(action.payload).pipe(
            tap(() => new FetchUserRegisterSuccess()),
            catchError((err) => of(new FetchUserRegisterFail(err)))
        ))
    )
}