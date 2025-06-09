import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable(
    {
        providedIn: 'root'
    }
)
export abstract class ILoginService {
    abstract login(username: string, password: string): boolean;
    abstract logout(): void;
    abstract checkLoginStatus(): boolean;
    abstract getLoggedInUser(): string | undefined;
    abstract user$?: Subject<string | undefined>;
}