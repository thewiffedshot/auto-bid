import { Component, Input } from '@angular/core';
import { UserModel } from '../models/user-model';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { UserListingComponent } from '../user-listing/user-listing.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-users-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, UserListingComponent],
  templateUrl: './users-dashboard.component.html',
  styleUrl: './users-dashboard.component.scss'
})
export class UsersDashboardComponent {
  @Input() users: UserModel[] = [];

  constructor(
    private readonly httpClient: HttpClient, 
  ) {
    this.httpClient.get<UserModel[]>('/api/User').subscribe(users => {
      this.users = users;
    });
  }
}
