import { Subject } from "rxjs";
import { ILoginService } from "./login.service.interface";
import { Injectable } from "@angular/core";
 
@Injectable(
    {
        providedIn: 'root'
    }
)
export class LoginServiceMock implements ILoginService {
    existingUsers: string[] = ['resonate', 'user1', 'user2'];

    private isLoggedIn: boolean = false;
    
    user$?: Subject<string | undefined> = new Subject<string | undefined>();

    constructor() {}

    /**
     * Simulates a login process.
     * @param username The username to log in with.
     * @param password The password (not used in this mock).
     * @returns true if the user is logged in, false otherwise.
     */    
    login(username: string, password?: string): boolean {
        if (this.isLoggedIn) {
            return true; // Already logged in
        }

        // Simulate a login check
        if (username && this.existingUsers.includes(username)) {
            if (this.user$) {
                this.user$.next(username); // Notify subscribers of the new user
                this.isLoggedIn = true;
                return true; // Login successful
            }
        }

        return false;
    }
    
    logout(): void {
        this.isLoggedIn = false;
        if (this.user$) {
            this.user$.next(undefined); // Notify subscribers of logout
        }
    }
    
    checkLoginStatus(): boolean {
        return this.isLoggedIn;
    }

    getLoggedInUser(): string | undefined {
        if (!this.user$) {
            return undefined; // No user observable available
        }

        // If the user is logged in, return the last value from the user observable
        if (this.isLoggedIn) {
            this.user$.subscribe((user) => {
                if (user) {
                    return user; // Return the logged-in user
                }

                return undefined; // No user logged in
            }).unsubscribe(); // Unsubscribe immediately to avoid memory leaks
        }

        return undefined; // Not logged in
    }
}