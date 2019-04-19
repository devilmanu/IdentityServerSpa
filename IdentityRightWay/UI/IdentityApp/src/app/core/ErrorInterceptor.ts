import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { tap, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { IdentityRightWayResponseBase } from '../models/IdentityRightWayResponseBase';
import { IdentityRightWayException } from '../models/IdentityRightWayException';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private _injector: Injector) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((error: HttpErrorResponse) => {
                let errorMessage : string | string[];
                if (error.error instanceof ErrorEvent) {
                    // client-side error
                    errorMessage = `Error: ${error.error.message}`;
                } else {
                    if(typeof<IdentityRightWayResponseBase<IdentityRightWayException>>(error.error)){
                        let errorBackend = error.error as IdentityRightWayResponseBase<IdentityRightWayException>
                        debugger
                        errorMessage = errorBackend.errors
                    }else{
                        errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
                    }
                }
                return throwError(errorMessage);
            })
        )
    }
}