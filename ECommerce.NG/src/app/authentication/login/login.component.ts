import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthenticationService } from 'src/app/services/auth.service';
import { UserLoginDto } from 'src/app/user/userLoginDto';
import { BaseResponseDto } from 'src/app/user/baseResponseDto';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  private returnUrl!: string;

  loginForm!: FormGroup;
  errorMessage!: string;
  showError!: boolean;
  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl(""),
      password: new FormControl("")
    })
    this.returnUrl = this.route.snapshot.queryParams[''] || '/';
  }


  loginUser = (loginFormValue: any) => {
    this.showError = false;
    const login = {... loginFormValue };
    const userForAuth: UserLoginDto = {
      username: login.username,
      password: login.password
    }
    this.authService.loginUser('Login', userForAuth)
    .subscribe({
      next: (res:BaseResponseDto) => {
       localStorage.setItem("token", res.result);
       this.authService.sendAuthStateChangeNotification(res.status == 200);
       this.router.navigate([this.returnUrl]);
    },
    error: (err:HttpErrorResponse) => {
      var response  = err.error as BaseResponseDto;
      this.errorMessage = response.errors[0].message;
      this.showError = true;
    }})
  }

}
