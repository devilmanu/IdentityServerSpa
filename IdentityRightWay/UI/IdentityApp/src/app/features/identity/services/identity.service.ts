import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IdentityRightWayResponseBase } from 'src/app/models/IdentityRightWayResponseBase';
import { LoginDto } from 'src/app/models/LoginDto';
import { mergeMap, delay } from 'rxjs/operators';
import { of, throwError } from 'rxjs';
import { RegisterCommand } from 'src/app/models/RegisterCommand';

@Injectable()
export class IdentityService {

  constructor(private http: HttpClient) { }

  login(email : string, password : string){
    const formData = new FormData();
    formData.append('email', email);
    formData.append('password', password);
    return this.http.post<IdentityRightWayResponseBase<LoginDto>>('https://localhost:5001/api/identity/login', formData).pipe(
      delay(3000)
    )
  }

  register(registerCommand : RegisterCommand){
    return this.http.post<void>('https://localhost:5001/api/identity/register', registerCommand).pipe(
      delay(3000)
    )
  }

  forgotPassword(){
    
  }
}
