import { Injectable, EventEmitter } from '@angular/core';
import { UiNotification } from './../models/ui-notification.model';
import { UiNotificationType } from '../enums/ui-notification-type.enum';

@Injectable()
export class UiNotificationService {
    public receiveNotification = new EventEmitter<UiNotification>();
    private notification: UiNotification;

    public showSuccess(content: string) {
        this.displayMessage(UiNotificationType.Success, content);
    }

    public showInfo(content: string) {
        this.displayMessage(UiNotificationType.Info, content);
    }

    public showWarn(content: string) {
        this.displayMessage(UiNotificationType.Warning, content);
    }

    public showError(content: string) {
        this.displayMessage(UiNotificationType.Error, content);
    }

    private displayMessage(type: UiNotificationType, content: string) {
        this.notification = new UiNotification(type, content);
        this.autoHide(4);
        this.receiveNotification.emit(this.notification);
    }

    private autoHide(time: number) {
        setTimeout(() => {
            this.notification.isActive = false;
        }, time * 1000);
    }
}
