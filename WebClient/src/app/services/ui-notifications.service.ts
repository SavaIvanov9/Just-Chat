import { Injectable } from '@angular/core';

@Injectable()
export class UiNotificationService {
    public showSuccess(content: string) {
        this.displayMessage('Success', content);
    }

    public showInfo(content: string) {
        this.displayMessage('Info', content);
    }

    public showWarn(content: string) {
        this.displayMessage('Warning', content);
    }

    public showError(content: string) {
        this.displayMessage('Error', content);
    }

    private displayMessage(type: string, content: string) {
        alert(`${type}\r\n${content}`);
    }
}
