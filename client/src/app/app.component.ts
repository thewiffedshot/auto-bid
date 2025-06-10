import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { LoginServiceMock } from './services/login.service';
import { OnInit } from '@angular/core';
import { LoginServiceMock } from './services/login.service';
import { OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  providers: [
    { provide: 'ILoginService', useClass: LoginServiceMock } // Replace with actual implementation
  ],
  providers: [
    { provide: 'ILoginService', useClass: LoginServiceMock } // Replace with actual implementation
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'AutoBid';

  loggedIn: boolean = false;

  @ViewChild('username') usernameInput!: ElementRef;

  set activeNavElement(value: 'home' | 'users' | 'offers' | 'createOffer' | 'createUser' | 'login') {
    let targetRoute = '';

    switch (value) {
      case 'home':
        targetRoute = '/dashboard';
        break;
      case 'users':
        targetRoute = '/users';
        break;
      case 'offers':
        targetRoute = '/offers';
        break;
      case 'createOffer':
        targetRoute = '/offer/create';
        break;
      case 'createUser':
        targetRoute = '/user/create';
        break;
      case 'login':
        targetRoute = '/dashboard';
        break;
    }

    this.router.navigate([targetRoute]);
  };

  constructor(
    private readonly router: Router,
    readonly loginService: LoginServiceMock) {
  constructor(
    private readonly router: Router,
    readonly loginService: LoginServiceMock) {
    this.activeNavElement = 'home';
  }

  ngOnInit(): void {
    this.loginService.user$?.subscribe(user => {
      if (user) {
        console.log(`User logged in: ${user}`);
        this.activeNavElement = 'home'; // Navigate to home when user logs in
        this.loggedIn = true;
      } else {
        console.log('No user logged in');
        this.activeNavElement = 'login'; // Navigate to login when no user is logged in
        this.loggedIn = false;
      }
    });
  }

  login(): void {
    this.loginService.login(this.usernameInput.nativeElement.value);
    this.activeNavElement = 'home'; // Navigate to home after login
  }

  logout(): void {
    this.loginService.logout();
  }
}
