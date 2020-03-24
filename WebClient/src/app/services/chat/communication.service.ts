import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

import { ChatUrl, ChatReconnectInterval } from '../../constants/configuration';
import { CreateMessageRequest } from '../../models/create-message-request.model';
import { CreateMessageResponse } from '../../models/create-message-response.model';
import { LoggerService } from '../logger.service';
import { UiNotificationService } from '../ui-notifications.service';
import { CurrentUserService } from '../authentication/current-user.service';
import * as signalR from '@aspnet/signalr';

@Injectable()
export class CommunicationService {
    receiveMessage = new EventEmitter<CreateMessageResponse>();
    receiveError = new EventEmitter<any>();
    connectionEstablished = new EventEmitter<boolean>();

    private isConnectionEstablished = false;
    private hubConnection: HubConnection;

    constructor(
        private logger: LoggerService,
        private uiNotifications: UiNotificationService,
        private currentUserService: CurrentUserService) {
        this.createConnection();
        this.registerOnServerEvents();
    }

    public get isConnected(): boolean {
        return this.isConnectionEstablished;
    }

    public sendMessage(
        message: CreateMessageRequest,
        onSuccess: () => void = null,
        onError: () => void = null): void {
        if (this.CheckIsConnected() === false) {
            return;
        }

        this.hubConnection
            .invoke('CreateMessage', message)
            .then(result => {
                if (onSuccess) {
                    onSuccess();
                }
            })
            .catch(error => {
                this.logger.logError(error);
                this.uiNotifications.showError('Could not send message.');
                if (onError) {
                    onError();
                }
            });
    }

    public joinRoom(
        roomId: number,
        onSuccess: () => void = null,
        onError: () => void = null): void {
        if (this.CheckIsConnected() === false) {
            return;
        }

        this.hubConnection
            .invoke('JoinRoom', roomId)
            .then(result => {
                if (onSuccess) {
                    onSuccess();
                }
            })
            .catch(error => {
                this.logger.logError(error);
                this.uiNotifications.showError('Could not join room.');
                if (onError) {
                    onError();
                }
            });
    }

    public leaveRoom(
        roomId: number,
        onSuccess: () => void = null,
        onError: () => void = null): void {
        if (this.CheckIsConnected() === false) {
            return;
        }

        this.hubConnection
            .invoke('LeaveRoom', roomId)
            .then(result => {
                if (onSuccess) {
                    onSuccess();
                }
            })
            .catch(error => {
                this.logger.logError(error);
                this.uiNotifications.showError('Could not leave room.');
                if (onError) {
                    onError();
                }
            });
    }

    public startConnection(
        onSuccess: () => void = null,
        onError: () => void = null): void {
        this.hubConnection
            .start()
            .then(() => {
                this.isConnectionEstablished = true;
                this.logger.logInfo('Hub connection started');
                this.connectionEstablished.emit(true);
                if (onSuccess) {
                    onSuccess();
                }
            })
            .catch(err => {
                this.logger.logError(`Error while establishing connection, retrying in ${ChatReconnectInterval / 1000} sec...`);
                setTimeout(() => { this.startConnection(); }, ChatReconnectInterval);
                if (onError) {
                    onError();
                }
            });
    }

    private createConnection(): void {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(ChatUrl, { accessTokenFactory: () => this.currentUserService.token })
            .configureLogging(signalR.LogLevel.Information)
            .build();
        this.hubConnection.serverTimeoutInMilliseconds = 10 * 60000;

        this.hubConnection
            .onclose(error => {
                this.startConnection();
            });
    }

    private registerOnServerEvents(): void {
        this.hubConnection
            .on('ReceiveMessage', (data: CreateMessageResponse) => {
                this.receiveMessage.emit(data);
            });

        this.hubConnection
            .on('HandleError', (error: any) => {
                this.receiveError.emit(error);
            });
    }

    private CheckIsConnected(): boolean {
        if (this.isConnectionEstablished === false) {
            this.uiNotifications.showError('Connection is not established.');
        }

        return this.isConnectionEstablished;
    }
}
