import { CurrentUserService } from 'src/app/services/authentication/current-user.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers, RequestOptionsArgs } from '@angular/http';
import { Observable, of, empty } from 'rxjs';
import { map, take, catchError } from 'rxjs/operators';

import { LoggerService } from './logger.service';
import { UiNotificationService } from './ui-notifications.service';

@Injectable()
export class HttpClientService {
    constructor(
        private http: Http,
        private logger: LoggerService,
        private router: Router,
        private currentUserService: CurrentUserService
    ) { }

    public get(
        url: string,
        successCallback: any = null,
        errorCallback: any = null
    ) {
        const headers = this.createAuthorizationHeader();

        return this.handleRequest(
            this.http.get(url, { headers }),
            successCallback,
            errorCallback
        );
    }

    public post(
        url,
        data,
        successCallback: any = null,
        errorCallback: any = null
    ) {
        const headers = this.createAuthorizationHeader();

        return this.handleRequest(
            this.http.post(url, data, { headers }),
            successCallback,
            errorCallback
        );
    }

    public put(url, data) {
        const headers = this.createAuthorizationHeader();
        return this.http.put(url, data, { headers });
    }

    public delete(url) {
        const headers = this.createAuthorizationHeader();
        return this.http.delete(url, { headers });
    }

    private handleRequest(request, successCallback, errorCallback) {
        return request.pipe(
            map((result: Response) => {
                if (successCallback) {
                    return successCallback(result);
                }

                return result;
            }),
            catchError((error: any) => {
                if (this.getStatusGroup(error.status) === '5') {
                    this.logger.logError(error);
                } else if (this.getStatusGroup(error.status) === '4') {
                    this.logger.logInfo(error);
                }

                if (errorCallback) {
                    return errorCallback();
                }

                return of(empty);
            })
        );
    }

    private getStatusGroup(statusCode: number) {
        if (statusCode) {
            return statusCode.toString()[0];
        }
    }

    private createAuthorizationHeader(): Headers {
        const headers = new Headers({ 'Content-Type': 'application/json' });

        if (this.currentUserService.token) {
            headers.set('Bearer', this.currentUserService.token);
        }

        return headers;
    }
}
