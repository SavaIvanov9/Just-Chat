import { Component, OnInit, OnDestroy, } from '@angular/core';
import { Router } from '@angular/router';

import { CreateMessageRequest } from '../../models/create-message-request.model';
import { CreateMessageResponse } from '../../models/create-message-response.model';
import { GetRoomResponse } from '../../models/get-room-response.model';
import { RoomsService } from '../../services/chat/rooms.service';
import { CommunicationService } from '../../services/chat/communication.service';
import { CurrentUserService } from '../../services/authentication/current-user.service';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.scss']
})
export class RoomsComponent implements OnInit, OnDestroy {
  title = 'JustChat';
  txtMessage = '';
  message = new CreateMessageRequest();
  rooms: Array<GetRoomResponse> = [];

  constructor(
    private communicationService: CommunicationService,
    private roomsService: RoomsService,
    private currentUserService: CurrentUserService,
    private router: Router
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
      this.rooms = rooms;
      this.roomsService.joinRoom(rooms[0]);
    };

    this.communicationService.startConnection(() => this.roomsService.getAllRooms(onSuccess));
  }

  ngOnDestroy(): void {
    this.roomsService.leaveCurrentRoom();
  }

  public sendMessage(): void {
    if (this.txtMessage) {

      this.roomsService.sendMessage(this.txtMessage);
      this.txtMessage = '';
    }
  }

  public joinRoom(room: GetRoomResponse): void {
    this.roomsService.joinRoom(room);
  }

  public logout() {
    this.roomsService.leaveCurrentRoom(
      () => {
        this.currentUserService.clearData();
        this.router.navigate(['/home']);
      },
      () => {
        this.currentUserService.clearData();
        this.router.navigate(['/home']);
      });
  }
}
