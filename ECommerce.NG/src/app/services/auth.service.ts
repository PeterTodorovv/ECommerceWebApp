import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { UserForRegistrationDto } from '../user/userForRegistrationDto';
import { EnvironmentUrlService } from './environment-url.service';
import { UserLoginDto } from '../user/userLoginDto';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService} from '@auth0/angular-jwt'
import { BaseResponseDto } from '../user/baseResponseDto';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  private userPayload: any;

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, private rout: Router) {
    this.userPayload = this.decodedToken();
  }


  public registerUser = (route: string, body: UserForRegistrationDto) => {
    return this.http.post<BaseResponseDto> (this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }
  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
  public loginUser = (route: string, body: UserLoginDto) => {
    return this.http.post<BaseResponseDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }
  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
    this.rout.navigate(['']);
  }

  public getToken(){
    return localStorage.getItem('token')
  }

  public isLoggedIn(): boolean{
    return !!this.getToken();
  }

  public decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    const decoded = jwtHelper.decodeToken(token);
    return decoded;
  }

  public getUsernameFromToken(){
    if(this.userPayload){
      return this.userPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    }
  }

  public getRoleFromToken(){
    if(this.userPayload){
      return this.userPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }
  }
}
