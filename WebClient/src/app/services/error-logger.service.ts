import { Injectable } from '@angular/core';

@Injectable()
export class ErrorLoggerService {

    constructor() {
    }

    public log(error: any) {
        console.log(error);
    }
}
