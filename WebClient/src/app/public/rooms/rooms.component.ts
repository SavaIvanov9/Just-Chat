import { Component, OnInit, NgZone } from '@angular/core';

import { CreateMessageRequest } from '../../models/create-message-request.model';
import { CreateMessageResponse } from '../../models/create-message-response.model';
import { GetRoomResponse } from 'src/app/models/get-room-response.model';
import { RoomsService } from 'src/app/services/chat/rooms.service';
import { CommunicationService } from 'src/app/services/chat/communication.service';
import { CurrentUserService } from 'src/app/services/authentication/current-user.service';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.scss']
})
export class RoomsComponent implements OnInit {
  title = 'JustChat';
  txtMessage = '';
  message = new CreateMessageRequest();

  constructor(
    private communicationService: CommunicationService,
    private roomsService: RoomsService,
    private ngZone: NgZone,
    private currentUserService: CurrentUserService
  ) {
  }

  public get messages(): Array<CreateMessageResponse> {
    return this.roomsService.messages;
  }

  public get userId(): number {
    return this.currentUserService.userId;
  }

  public get username(): string {
    return this.currentUserService.username;
  }

  ngOnInit(): void {
    const onSuccess = (rooms: Array<GetRoomResponse>) => {
      this.roomsService.joinRoom(rooms[0].id);
    };

    this.communicationService.connectionEstablished.subscribe(
      (result: boolean) => {
        if (result) {
          this.roomsService.getAllRooms(onSuccess);
        }
      });
  }

  public sendMessage(): void {
    if (this.txtMessage) {

      this.roomsService.sendMessage(this.txtMessage);
      this.txtMessage = '';
    }
  }
}
