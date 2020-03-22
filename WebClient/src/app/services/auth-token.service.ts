import { Injectable } from '@angular/core';

@Injectable()
export class AuthTokenService {

    private tokenName: string;

    constructor() {
        this.tokenName = 'df4ee67j534y5hfej4tdgnf';
    }

    getToken() {
        return localStorage.getItem(this.tokenName);
    }

    setToken(value: string) {
        localStorage.setItem(this.tokenName, value);
    }


    clearData(): void {
        localStorage.removeItem(this.tokenName);
    }
}
