import { Injectable } from '@angular/core';

@Injectable()
export class UiNotificationService {
    public showSuccess(title: string, content: string = null) {
        this.displayError('Success', title, content);
    }

    public showInfo(title: string, content: string = null) {
        this.displayError('Info', title, content);
    }

    public showWarn(title: string, content: string = null) {
        this.displayError('Warning', title, content);
    }

    public showError(title: string, content: string = null) {
        this.displayError('Error', title, content);
    }

    private displayError(type: string, title: string, content: string = null) {
        alert(`${type}: ${title}\r\n${content}`);
    }
}
