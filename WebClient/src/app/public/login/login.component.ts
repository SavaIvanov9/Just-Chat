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

  UsernameFormControl = new FormControl(this.loginData.password, [
    Validators.pattern(UserNameRegex),
    Validators.required
  ]);

  PasswordFormControl = new FormControl(this.loginData.password, [
    Validators.required,
    Validators.pattern(PasswordRegex),
    Validators.maxLength(30),
    Validators.minLength(5)
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
    this.loginData.username = this.UsernameFormControl.value;
    this.loginData.password = this.PasswordFormControl.value;
  }

  public validateLoginData(): boolean {
    const hasError =
      this.UsernameFormControl.hasError('required') ||
      this.UsernameFormControl.hasError('pattern') ||
      this.PasswordFormControl.hasError('required') ||
      this.PasswordFormControl.hasError('pattern') ||
      this.PasswordFormControl.hasError('maxlength') ||
      this.PasswordFormControl.hasError('minlength');

    return hasError === false;
  }
}
