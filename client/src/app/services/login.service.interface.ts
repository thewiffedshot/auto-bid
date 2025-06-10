import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export abstract class ILoginService {
    abstract login(username: string, password?: string): boolean;
    abstract logout(): void;
    abstract checkLoginStatus(): boolean;
    abstract getLoggedInUser(): string | undefined;
    abstract user$?: Observable<string | undefined>;
}