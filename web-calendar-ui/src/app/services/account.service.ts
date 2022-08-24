import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Config } from '../config/config';
import { UserLoginModel } from '../models/user-login-model';
import { UserRegistrationModel } from '../models/user-registration-model';
import { UserViewModel } from '../models/user-view-model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private accountApi = this.config.setting['PathAPI'] + 'Account/';
  private user: UserViewModel;

  constructor(private router: Router,
    private http: HttpClient, private config: Config) {
    let user = localStorage.getItem('currentUser');
    if (user) {
      this.user = JSON.parse(user);
    }
  }

  get userValue(): UserViewModel {
    return this.user;
  }

  login(userLoginModel: UserLoginModel): Observable<UserViewModel> {
    return this.http.post<UserViewModel>(this.accountApi + 'Login', userLoginModel).pipe(map(user => {
      localStorage.setItem('currentUser', JSON.stringify(user));
      this.user = user;
      return user;
    }));
  }

  logout() {
    this.user = undefined;
    localStorage.removeItem('currentUser');
    this.router.navigate(['/login']);
  }

  register(userRegsiterModel: UserRegistrationModel): Observable<UserViewModel> {
    return this.http.post<UserViewModel>(this.accountApi + 'Register', userRegsiterModel).pipe(map(user => {
      localStorage.setItem('currentUser', JSON.stringify(user));
      this.user = user;
      return user;
    }));
  }

  update() {
    //here update account stat
  }
}