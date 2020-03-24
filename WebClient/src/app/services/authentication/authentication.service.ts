import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Router } from '@angular/router';
import { Observable, of, empty } from 'rxjs';
import { map, take, catchError } from 'rxjs/operators';

import { RegisterUrl, TokenUrl, LoginUrl } from '../../constants/configuration';

import { CurrentUserService } from './current-user.service';
import { HttpClientService } from '../http-client.service';

import { RegisterRequest } from '../../models/register-request.model';
import { RegisterResponse } from '../../models/register-response.model';
import { GetUserResponse } from '../../models/get-user-response.model';
import { LoginRequest } from '../../models/login-request.model';
import { LoginResponse } from '../../models/login-response.model';

@Injectable()
export class AuthenticationService {
    private isAuthenticated = false;

    constructor(
        private currentUserService: CurrentUserService,
        private httpClient: HttpClientService,
        private router: Router
    ) { }

    public get IsAuthenticated(): boolean {
        return this.isAuthenticated;
    }

    public register(
        registerData: RegisterRequest,
        successCallback: any = null,
        errorCallback: any = null): void {
        this.httpClient.post(
            RegisterUrl, registerData,
            (result: Response) => {
                const response: RegisterResponse = result.json();
                if (response.token) {
                    this.isAuthenticated = true;
                    this.currentUserService.token = response.token;
                    this.currentUserService.userId = response.userId;
                    this.currentUserService.username = registerData.username;
                }
                if (successCallback) {
                    return successCallback(response);
                }
            },
            () => {
                this.isAuthenticated = false;

                if (errorCallback) {
                    return errorCallback();
                }

                return of(empty);
            }
        ).subscribe();
    }

    login(
        loginData: LoginRequest,
        successCallback: any = null,
        errorCallback: any = null
    ) {
        this.httpClient.post(
            LoginUrl, loginData,
            (result: Response) => {
                const response: LoginResponse = result.json();
                if (response.token) {
                    this.isAuthenticated = true;
                    this.currentUserService.token = response.token;
                    this.currentUserService.userId = response.userId;
                    this.currentUserService.username = loginData.username;
                }
                if (successCallback) {
                    return successCallback(response);
                }
            },
            () => {
                this.isAuthenticated = false;

                if (errorCallback) {
                    return errorCallback();
                }

                return of(empty);
            }
        ).subscribe();
    }

    public validateToken(
        successCallback: any = null,
        errorCallback: any = null) {
        return this.httpClient.get(
            TokenUrl,
            (response: GetUserResponse) => {
                this.isAuthenticated = true;
                if (successCallback) {
                    return successCallback();
                }

                this.currentUserService.userId = response.userId;
                this.currentUserService.username = response.username;
            },
            () => {
                this.isAuthenticated = false;
                if (errorCallback) {
                    return errorCallback();
                }
            }
        );
    }

    public signOut(): void {
        this.currentUserService.clearData();
    }
}
