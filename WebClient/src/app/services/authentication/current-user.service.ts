import { Injectable } from '@angular/core';

@Injectable()
export class CurrentUserService {
    private tokenKey: string;
    private userIdKey: string;
    private userNameKey: string;

    constructor() {
        this.tokenKey = 'df4ee67j534y5hfej4tdgnf';
        this.userIdKey = 'dsfsdfdsfxcbcvbfbdfbfdg';
        this.userNameKey = 'sdfsdfxcry5efdtydfgrerm';
    }

    public get token(): string {
        return localStorage.getItem(this.tokenKey);
    }

    public set token(value: string) {
        localStorage.setItem(this.tokenKey, value);
    }

    public get userId(): number {
        return +localStorage.getItem(this.userIdKey);
    }

    public set userId(value: number) {
        localStorage.setItem(this.userIdKey, String(value));
    }

    public get username(): string {
        return localStorage.getItem(this.userNameKey);
    }

    public set username(value: string) {
        localStorage.setItem(this.userNameKey, value);
    }

    public clearData(): void {
        localStorage.removeItem(this.tokenKey);
        localStorage.removeItem(this.userIdKey);
        localStorage.removeItem(this.userNameKey);
    }
}
