import { Component, Input } from '@angular/core';
import { UserModel } from '../models/user-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-listing',
  standalone: true,
  imports: [],
  templateUrl: './user-listing.component.html',
  styleUrl: './user-listing.component.scss'
})
export class UserListingComponent {
  @Input() user!: UserModel;
  
  constructor(
    private readonly router: Router
  ) { }

  openUser(): void {
    this.router.navigate(['/user', this.user.username], { state: { openedUser: this.user } });
  }
}
