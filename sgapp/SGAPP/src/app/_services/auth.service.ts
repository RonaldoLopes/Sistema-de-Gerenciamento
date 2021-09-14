import { Injectable, Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { Role } from '../_models/Role';
import { Observable } from 'rxjs';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  ipAddress: any;

  baseURL = 'http://localhost:56979/api/login/Login/';
  baseURLRegist = 'http://localhost:56979/api/login/Register';
  baseURLPUTROLE = 'http://localhost:56979/api/Role/UpdateUserRole';
  baseURLGetUser = 'http://localhost:56979/api/login/getAllUser';
  baseURLUpdateUser = 'http://localhost:56979/api/login';

  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http
    .post(`${this.baseURL}`, model)
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          //sessionStorage.clear();
          localStorage.setItem('username', this.decodedToken.unique_name);
          localStorage.setItem('idUser', this.decodedToken.nameid);
          localStorage.setItem('role', this.decodedToken.role);
        }
      })
    );
  }
  getAllUser(): Observable<User[]> {
    return this.http.get<User[]>(this.baseURLGetUser);
  }
  register(model: any) {
    return this.http.post(`${this.baseURLRegist}`, model);
  }
  putUser(user: User) {
    return this.http.post(`${this.baseURLUpdateUser}/${user.id}`, user);
  }
  putUserRole(role: Role) {
    return this.http.put(`${this.baseURLPUTROLE}/` , role);
    //return this.http.put(`${this.baseURL}` , user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
