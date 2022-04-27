import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {tap} from 'rxjs/operators';
import { environment } from 'src/environments/environment';

import { LoginForm } from '../interfaces/login-form.interface';
import { RegisterForm } from '../interfaces/register-form.interface';

const url = `${environment.UrlApiUser}user/`;

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  createUser(data: RegisterForm){
    return this.http.post(`${url}register`, data).pipe(
      tap(resp =>{
        localStorage.setItem('login', JSON.stringify(resp));
        localStorage.removeItem('email');
      })
    );
  }

  login(data: LoginForm){
    return this.http.post(`${url}login`, data).pipe(
      tap(resp =>{
        localStorage.setItem('login', JSON.stringify(resp));
      })
    );
  }

  logout(){
    localStorage.removeItem('login');
  }
}
