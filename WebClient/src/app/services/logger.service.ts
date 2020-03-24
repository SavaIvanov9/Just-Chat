import { Injectable } from '@angular/core';

@Injectable()
export class LoggerService {

    constructor() {
    }

    public logInfo(info: any) {
        console.log(info);
    }

    public logError(error: any) {
        console.log(error);
    }
}
