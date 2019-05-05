import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IdentityRightWayResponseBase } from 'src/app/models/IdentityRightWayResponseBase';
import { LoginDto } from 'src/app/models/LoginDto';
import { mergeMap, delay } from 'rxjs/operators';
import { of, throwError } from 'rxjs';
import { RegisterCommand } from 'src/app/models/RegisterCommand';
import { ConsentDto } from 'src/app/models/ConsentDto';
import { AcceptConsentCommand } from 'src/app/models/AcceptConsentCommand';
import { AcceptConsentDto } from 'src/app/models/AcceptConsentDto';
import { GetConsentQuery } from 'src/app/models/GetConsentQuery';

@Injectable()
export class IdentityService {

  constructor(private http: HttpClient) { }

  login(email : string, password : string, returnUrl : string){
    const formData = new FormData();
    formData.append('email', email);
    formData.append('password', password);
    formData.append('returnUrl', returnUrl);
    return this.http.post<IdentityRightWayResponseBase<LoginDto>>('https://localhost:5002/api/identity/login', formData).pipe(
    )
  }

  register(registerCommand : RegisterCommand){
    return this.http.post<void>('https://localhost:5002/api/identity/register', registerCommand).pipe(
    )
  }

  forgotPassword(){
    
  }

  getConsent(getConsentQuery : GetConsentQuery){
    return this.http.get<IdentityRightWayResponseBase<ConsentDto>>('https://localhost:5002/api/Consent/get', { params : <any>getConsentQuery }).pipe(
    )
  }

  acceptConsent(acceptConsentCommand : AcceptConsentCommand){
    return this.http.post<IdentityRightWayResponseBase<AcceptConsentDto>>('https://localhost:5002/api/Consent/accept', acceptConsentCommand).pipe(
    )
  }
}
