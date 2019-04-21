import { Injectable } from '@angular/core';
import { FetchUserLogin, FetchUserLoginSuccess, FetchUserLoginFail, IdentityBackendActionsTypes, FetchUserRegister, FetchUserRegisterFail, FetchUserRegisterSuccess, FetchConsentSuccess, FetchConsent, FetchConsentFail, FetchAcceptConsent, FetchAcceptConsentSuccess, FetchAcceptConsentFail } from '../actions/backend.actions';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { IdentityService } from '../../services/identity.service';
import { switchMap, catchError, mergeMap, tap } from 'rxjs/operators';
import { of, throwError, from } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class BackendEffects
{
    constructor(
        private actions$: Actions, 
        private identityApiService : IdentityService, 
        private router : Router)
    {

    }
    
    @Effect()
    FetchUserLogin$ = this.actions$.pipe(
        ofType<FetchUserLogin>(IdentityBackendActionsTypes.FetchUserLogin),
        switchMap((action) => this.identityApiService.login(action.payload.email, action.payload.password,action.payload.returnUrl).pipe(
            mergeMap(o => o.isValid 
                ? of(window.open(action.payload.returnUrl, '_self')) 
                : throwError(o.errors)),
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

    @Effect()
    FetchConsent$ = this.actions$.pipe(
        ofType<FetchConsent>(IdentityBackendActionsTypes.FetchConsent),
        switchMap((action) => this.identityApiService.getConsent(action.payload).pipe(
            mergeMap(o => o.isValid 
                ? of(new FetchConsentSuccess(o)) 
                : throwError(o.errors)),
            catchError((err) => of(new FetchConsentFail(err)))
        ))
    )

    @Effect()
    FetchAcceptConsent$ = this.actions$.pipe(
        ofType<FetchAcceptConsent>(IdentityBackendActionsTypes.FetchAcceptConsent),
        switchMap((action) => this.identityApiService.acceptConsent(action.payload).pipe(
            mergeMap(o => o.isValid 
                ? of(window.open(o.payload.redirectUri, '_self'))
                : throwError(o.errors)),
            catchError((err) => of(new FetchAcceptConsentFail(err)))
        ))
    )
}