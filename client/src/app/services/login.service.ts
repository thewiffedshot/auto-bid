import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ILoginService } from './login.service.interface';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoginServiceMock implements ILoginService {
  existingUsers: string[] = ['resonate', 'user1', 'user2'];
  private userSubject = new BehaviorSubject<string | undefined>(undefined);

  get user$(): Observable<string | undefined> {
    return this.userSubject.asObservable();
  }

  login(username: string, password?: string): boolean {
    if (this.userSubject.value) {
      return true;
    }
    if (username && this.existingUsers.includes(username)) {
      this.userSubject.next(username);
      return true;
    }
    return false;
  }

  logout(): void {
    this.userSubject.next(undefined);
  }

  checkLoginStatus(): boolean {
    return !!this.userSubject.value;
  }

  getLoggedInUser(): string | undefined {
    return this.userSubject.value;
  }
}
