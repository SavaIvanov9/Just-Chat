import { Injectable, NgZone } from '@angular/core';
import { of, empty } from 'rxjs';

import { RoomUrl } from '../../constants/configuration';
import { CreateMessageRequest } from '../../models/create-message-request.model';
import { CreateMessageResponse } from '../../models/create-message-response.model';
import { GetRoomResponse } from 'src/app/models/get-room-response.model';
import { LoggerService } from '../logger.service';
import { UiNotificationService } from '../ui-notifications.service';
import { CommunicationService } from './communication.service';
import { HttpClientService } from '../http-client.service';
import { CurrentUserService } from '../authentication/current-user.service';

@Injectable()
export class RoomsService {
    private currentRoom: GetRoomResponse;
    private messageResponses = new Array<CreateMessageResponse>();

    constructor(
        private logger: LoggerService,
        private uiNotifications: UiNotificationService,
        private communicationService: CommunicationService,
        private httpClient: HttpClientService,
        private ngZone: NgZone,
        private currentUserService: CurrentUserService) {
        this.subscribeToEvents();
    }

    public get messages(): Array<CreateMessageResponse> {
        return this.messageResponses;
    }

    public get currentRoomName(): string {
        return this.currentRoom.name;
    }

    public sendMessage(content: string): void {
        if (this.currentRoom) {
            const messageRequest = new CreateMessageRequest();
            messageRequest.roomId = this.currentRoom.id;
            messageRequest.content = content;

            this.communicationService.sendMessage(messageRequest);
        } else {
            this.uiNotifications.showWarn('You must join a room before send any messages');
        }
    }

    public getAllRooms(
        onSuccess: (rooms: Array<GetRoomResponse>) => void = null,
        onError: () => void = null): void {
        this.httpClient
            .get(
                RoomUrl,
                (result: any) => {
                    const response: GetRoomResponse[] = JSON.parse(result._body);
                    if (onSuccess) {
                        return onSuccess(response);
                    }
                },
                () => {
                    if (onError) {
                        return onError();
                    }
                }
            )
            .subscribe();
    }

    public joinRoom(room: GetRoomResponse): void {
        if (this.currentRoom) {
            this.leaveCurrentRoom();
        }

        this.communicationService.joinRoom(
            room.id,
            () => {
                this.messageResponses = [];
                this.currentRoom = room;
            });
    }

    public leaveCurrentRoom(
        onSuccess: () => void = null,
        onError: () => void = null): void {
        if (this.currentRoom) {
            this.communicationService.leaveRoom(
                this.currentRoom.id,
                () => {
                    this.currentRoom = null;
                    if (onSuccess) {
                        onSuccess();
                    }
                },
                () => {
                    if (onError) {
                        onError();
                    }
                });
        }
    }

    private subscribeToEvents(): void {
        this.communicationService.receiveMessage.subscribe((result: CreateMessageResponse) => {
            this.ngZone.run(() => {
                this.messageResponses.push(result);
            });
        });

        this.communicationService.receiveError.subscribe((result: any) => {
            this.ngZone.run(() => {
                this.logger.logError(result);
            });
        });
    }
}
