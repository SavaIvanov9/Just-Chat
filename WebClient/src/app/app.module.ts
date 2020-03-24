import { BrowserModule, DomSanitizer } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AngularMaterialModule } from './external-modules/angular-material.module';
import { AppRoutingModule } from './app-routing.module';
import { ControlsModule } from './controls/controls.module';

import { AuthenticationGuard } from './guards/authentication-guard';

import { HttpClientService } from './services/http-client.service';
import { AuthenticationService } from './services/authentication/authentication.service';
import { CurrentUserService } from './services/authentication/current-user.service';
import { LoggerService } from './services/logger.service';
import { CommunicationService } from './services/chat/communication.service';
import { UiNotificationService } from './services/ui-notifications.service';
import { RoomsService } from './services/chat/rooms.service';

import { AppComponent } from './app.component';
import { RoomsComponent } from './public/rooms/rooms.component';
import { RegisterComponent } from './public/register/register.component';
import { MatIconRegistry } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    RoomsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AngularMaterialModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    ControlsModule,
  ],
  providers: [
    AuthenticationService,
    CurrentUserService,
    LoggerService,
    HttpClientService,
    AuthenticationGuard,
    UiNotificationService,
    CommunicationService,
    RoomsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(matIconRegistry: MatIconRegistry, domSanitizer: DomSanitizer) {
    matIconRegistry.addSvgIconSet(
      domSanitizer.bypassSecurityTrustResourceUrl('./assets/images/mdi.svg')
    );
  }
}
