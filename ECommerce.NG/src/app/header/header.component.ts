import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, observable } from "rxjs";
import { AuthenticationService } from "../services/auth.service";
import { UserStoreServiceTsService } from "../services/user-store.service.ts.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})

export class HeaderComponent{
  public isUserAuthenticated!: boolean;

  public username : string = "";
  public role! : string;

  constructor(private authService: AuthenticationService, private router: Router, private userStore: UserStoreServiceTsService) { }
  ngOnInit(): void {
    this.authService.authChanged
    .subscribe(res => {
      this.isUserAuthenticated = this.authService.isLoggedIn();
    });

    this.userStore.getUsernameFromStore()
    .subscribe(val => {
      let getUsernameFromToken = this.authService.getUsernameFromToken();
      this.username = val || getUsernameFromToken;
    })

    this.userStore.getRoleFromStore()
    .subscribe(val => {
      const roleFromToken = this.authService.getRoleFromToken();
      this.role = val || roleFromToken;
    })
  }
  public logout = () => {
    this.authService.logout();
    this.router.navigate(["/"]);
  }

  public isLoggedIn(){
    return this.authService.isLoggedIn();
  }

}
