import { Component, ViewChild } from '@angular/core';
import { initialUserModel, UserModel } from '../models/user-model';
import { UserDetailsComponent } from '../user-details/user-details.component';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-user-create',
  standalone: true,
  imports: [UserDetailsComponent],
  templateUrl: './user-create.component.html',
  styleUrl: './user-create.component.scss'
})
export class UserCreateComponent {
  private _user!: UserModel;
  
  get user(): UserModel {
    return this._user || initialUserModel;
  }

  set user(value: UserModel) {
    this._user = value;
  }
  
  @ViewChild(UserDetailsComponent) offerDetailsComponent!: UserDetailsComponent;

  private subscription?: Subscription;

  constructor(
    private readonly router: Router,
    private readonly httpClient: HttpClient
  ) {
    this._user = this.router.getCurrentNavigation()?.extras.state?.['openedOffer'];    
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onCancelClick(): void {
    this.router.navigate(['/dashboard']);
  }

  onSaveClick(): void {
    this.httpClient.post<string>(`/api/User`, this.user).subscribe(_userId => {
      this.router.navigate(['/user/', this.user.username]);
    });
  }
}
