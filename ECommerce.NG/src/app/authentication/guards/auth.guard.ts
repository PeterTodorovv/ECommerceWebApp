import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthenticationService } from "src/app/services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate{
  constructor(private auth: AuthenticationService, private rout: Router){

  }
  canActivate(): boolean{
    if(this.auth.isLoggedIn()){
      return true;
    }
    else{
      this.rout.navigate(['']);
      return false;
    }
  }

  cantActivate(): boolean{
    if(this.auth.isLoggedIn()!){
      return true;
    }
    else{
      this.rout.navigate(['']);
      return false;
    }
  }
}
