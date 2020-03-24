import { Injectable } from '@angular/core';

@Injectable()
export class CurrentUserService {
    public userId: number;
    public username: string;
    private tokenKey: string;

    constructor() {
        this.tokenKey = 'df4ee67j534y5hfej4tdgnf';
    }

    public get token(): string {
        return localStorage.getItem(this.tokenKey);
    }

    public set token(value: string) {
        localStorage.setItem(this.tokenKey, value);
    }

    public clearData(): void {
        localStorage.removeItem(this.tokenKey);
        this.userId = null;
        this.username = null;
    }
}
