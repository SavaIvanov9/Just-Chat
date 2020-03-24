import { UiNotificationType } from '../enums/ui-notification-type.enum';

export class UiNotification {
  constructor(
    public type: UiNotificationType,
    public content: string
  ) {}

  public isActive = true;
}
