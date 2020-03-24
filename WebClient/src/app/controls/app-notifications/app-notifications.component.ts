import { Component, Inject, Input, OnInit } from '@angular/core';
import { UiNotification } from '../../models/ui-notification.model';
import { UiNotificationService } from 'src/app/services/ui-notifications.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './app-notifications.component.html',
  styleUrls: ['./app-notifications.component.scss']
})
export class AppNotificationsComponent implements OnInit {
  notification: UiNotification;

  constructor(private uiNotificationService: UiNotificationService) {
  }

  ngOnInit(): void {
    this.uiNotificationService.receiveNotification
      .subscribe(x => {
        this.notification = x;
      });
  }

  deactivate(notification: UiNotification) {
    notification.isActive = false;
  }
}
