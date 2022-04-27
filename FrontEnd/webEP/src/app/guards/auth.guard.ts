import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router){};
  canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      try {
        const user: User = new User(JSON.parse(localStorage.getItem('login') || '{}'));
        if(user.token !== undefined && user.token !== ''){
          return true;
        }else{
          this.router.navigateByUrl('/');
          return false;
        }
      } catch (error) {
        return false;
      }
  }
}
