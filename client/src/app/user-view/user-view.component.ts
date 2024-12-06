import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserModel } from '../models/user-model';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { UserDetailsComponent } from '../user-details/user-details.component';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-user-view',
  standalone: true,
  imports: [UserDetailsComponent],
  templateUrl: './user-view.component.html',
  styleUrl: './user-view.component.scss'
})
export class UserViewComponent implements OnDestroy, OnInit {
  public user?: UserModel;
  
  private subscription!: Subscription;

  constructor(
    private readonly router: Router,
    private readonly httpClient: HttpClient,
    private readonly route: ActivatedRoute
  ) {
    this.user = this.router.getCurrentNavigation()?.extras.state?.['openedUser'];
  }
  
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  ngOnInit(): void {
    if (!this.user) {
      this.subscription = this.route.params.subscribe(params => {
        if (!params['id']) {
          this.router.navigate(['/']);
          return;
        }

        this.httpClient.get<UserModel>(`${environment.apiUrl}/api/User/${params['id']}`).subscribe(user => {
          this.user = user;
        });
      });
    }
  }

  onDeleteClick(): void {
    this.httpClient.delete(`${environment.apiUrl}/api/User/${this.user?.id}`).subscribe(() => {
      this.router.navigate(['/users']);
    });
  }

  onCancelClick(): void {
    this.router.navigate(['/users']);
  }
}
