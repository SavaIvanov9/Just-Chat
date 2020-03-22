import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Router } from '@angular/router';
import { Observable, of, empty } from 'rxjs';
import { map, take, catchError } from 'rxjs/operators';

import { RegisterUrl } from '../constants/configuration';

import { AuthTokenService } from './auth-token.service';
import { HttpClientService } from '../services/http-client.service';

import { RegisterRequest } from '../models/register-request.model';
import { RegisterResponse } from '../models/register-response.model';

@Injectable()
export class AuthenticationService {
    private isAuthenticated = false;

    constructor(
        private authTokenService: AuthTokenService,
        private httpClient: HttpClientService,
        private router: Router
    ) { }

    public get IsAuthenticated(): boolean {
        return this.isAuthenticated;
    }

    register(
        registerData: RegisterRequest,
        successCallback: any = null,
        errorCallback: any = null
    ) {
        this.httpClient
            .post(
                RegisterUrl,
                registerData,
                (result: Response) => {
                    const response: RegisterResponse = result.json();
                    if (response.userId) {
                        this.isAuthenticated = true;
                        this.authTokenService.setToken(response.userId);
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
            )
            .subscribe();
    }

    signOut(): void {
        this.authTokenService.clearData();
    }
}
