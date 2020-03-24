import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';

import { UserNameRegex, PasswordRegex } from '../../constants/regexs';
import { LoginRequest } from '../../models/login-request.model';
import { LoginResponse } from '../../models/login-response.model';
import { AuthenticationService } from '../../services/authentication/authentication.service';
import { UiNotificationService } from '../../services/ui-notifications.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginData: LoginRequest = new LoginRequest();

  passwordFormControl = new FormControl(this.loginData.password, [
    Validators.required,
    Validators.pattern(PasswordRegex),
    Validators.maxLength(50),
    Validators.minLength(5)
  ]);

  usernameFormControl = new FormControl(this.loginData.username, [
    Validators.required,
    Validators.pattern(UserNameRegex),
    Validators.maxLength(50),
    Validators.minLength(1)
  ]);

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private uiNotificationService: UiNotificationService
  ) { }

  ngOnInit() {
  }

  public goToRegister(): void {
    this.router.navigate(['register']);
  }

  public login(): void {
    this.updateModel();

    if (this.validateLoginData()) {
      this.authService.login(
        this.loginData,
        (result: LoginResponse) => {
          if (result.token) {
            this.router.navigate(['rooms']);
            this.uiNotificationService.showSuccess('Logged in successfully');
          }
        },
        () => {
          this.uiNotificationService.showWarn('Invalid credentials');
        });
    }
  }

  public updateModel(): void {
    this.loginData.username = this.usernameFormControl.value;
    this.loginData.password = this.passwordFormControl.value;
  }

  public validateLoginData(): boolean {
    const hasError = this.ValidatePassword() || this.ValidateUserName();
    return hasError === false;
  }

  private ValidatePassword() {
    return (
      this.passwordFormControl.hasError('required') ||
      this.passwordFormControl.hasError('pattern') ||
      this.passwordFormControl.hasError('maxlength') ||
      this.passwordFormControl.hasError('minlength')
    );
  }

  private ValidateUserName() {
    return (
      this.usernameFormControl.hasError('required') ||
      this.usernameFormControl.hasError('pattern') ||
      this.usernameFormControl.hasError('maxlength') ||
      this.usernameFormControl.hasError('minlength')
    );
  }
}
