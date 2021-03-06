import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';

import {
  PasswordRegex,
  UserNameRegex
} from '../../constants/regexs';

import { RegisterRequest } from 'src/app/models/register-request.model';
import { RegisterResponse } from 'src/app/models/register-response.model';

import { UiNotificationService } from 'src/app/services/ui-notifications.service';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerData: RegisterRequest = new RegisterRequest();

  passwordFormControl = new FormControl(this.registerData.password, [
    Validators.required,
    Validators.pattern(PasswordRegex),
    Validators.maxLength(50),
    Validators.minLength(5)
  ]);

  usernameFormControl = new FormControl(this.registerData.username, [
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

  ngOnInit() { }

  register() {
    this.updateModel();

    if (this.validateInputData()) {
      this.authService.register(
        this.registerData,
        (result: RegisterResponse) => {
          if (result.token) {
            this.uiNotificationService.showSuccess('Successful registration');
            this.router.navigate(['rooms']);
          } else {
            this.uiNotificationService.showSuccess('Unsuccessful registration');
          }
        }
      );
    } else {
      this.uiNotificationService.showWarn('Invalid input');
    }
  }

  public updateModel() {
    this.registerData.username = this.usernameFormControl.value;
    this.registerData.password = this.passwordFormControl.value;
  }

  public validateInputData(): boolean {
    const hasError = this.ValidatePassword() || this.ValidateUserName();
    return hasError === false;
  }

  public goToLogin(): void {
    this.router.navigate(['login']);
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
