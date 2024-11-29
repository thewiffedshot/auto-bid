import { Routes } from '@angular/router';
import { OffersDashboardComponent } from './offers-dashboard/offers-dashboard.component';
import { OfferCreateComponent } from './offer-create/offer-create.component';
import { OfferModifyComponent } from './offer-modify/offer-modify.component';
import { OfferViewComponent } from './offer-view/offer-view.component';
import { UsersDashboardComponent } from './users-dashboard/users-dashboard.component';
import { UserCreateComponent } from './user-create/user-create.component';
import { UserModifyComponent } from './user-modify/user-modify.component';
import { UserViewComponent } from './user-view/user-view.component';

export const routes: Routes = [
  { path: 'dashboard', component: OffersDashboardComponent },
  {
    path: 'offer',
    children: [
      { path: 'create', component: OfferCreateComponent },
      { path: 'modify/:id', component: OfferModifyComponent },
      { path: ':id', component: OfferViewComponent },
    ],
  },
  { path: 'users', component: UsersDashboardComponent },
  {
    path: 'user',
    children: [
        { path: 'create', component: UserCreateComponent },
        { path: ':username', component: UserViewComponent},
    ],
  },
  { path: 'offers', component: OffersDashboardComponent },
  { path: 'home', redirectTo: '/dashboard' },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'https://http.cat/status/404' },
];
