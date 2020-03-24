import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './services/authentication/authentication.service';
import { UiNotificationService } from './services/ui-notifications.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
  constructor(
    private authService: AuthenticationService,
    public notificationService: UiNotificationService
  ) { }

  ngOnInit(): void {
    this.authService.validateToken();
  }
}
