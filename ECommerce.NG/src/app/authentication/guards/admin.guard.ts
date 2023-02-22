import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthenticationService } from "src/app/services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate{
  constructor(private auth: AuthenticationService, private rout: Router){

  }
  canActivate(): boolean{
    if(this.auth.getRoleFromToken() == "Admin"){
      return true;
    }
    else{
      this.rout.navigate(['']);
      return false;
    }
  }
}
