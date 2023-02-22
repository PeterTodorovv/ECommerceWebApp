import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/auth.service';
import { BaseResponseDto } from 'src/app/user/baseResponseDto';
import { UserForRegistrationDto } from 'src/app/user/userForRegistrationDto';

@Component({
  selector: 'app-register-user',
 templateUrl: './register-user.component.html',
})

export class RegisterUserComponent implements OnInit {
  private returnUrl!: string;
  registerForm!: FormGroup;
  errorMessage!: string;
  showError!: boolean;

  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      username: new FormControl(''),
      email: new FormControl(''),
      password: new FormControl('', [Validators.required]),
      adress: new FormControl('')
    });

    this.returnUrl = this.route.snapshot.queryParams['Login'] || '/Login';
  }

  public registerUser = (registerFormValue: any) => {
    const formValues = { ...registerFormValue };
    const user: UserForRegistrationDto = {
      firstName: formValues.firstName,
      username: formValues.username,
      lastName: formValues.lastName,
      email: formValues.email,
      password: formValues.password,
      address: formValues.address
    };

    this.authService.registerUser("Register", user)
    .subscribe({
      next: (res:BaseResponseDto) => {
        this.router.navigate([this.returnUrl]);
    },
    error: (err:HttpErrorResponse) => {
      var response  = err.error as BaseResponseDto;
      this.errorMessage = response.errors[response.errors.length - 1].message;
      this.showError = true;
    }})

  }
}
