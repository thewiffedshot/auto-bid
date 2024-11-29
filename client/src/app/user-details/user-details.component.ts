import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { initialUserModel, UserModel } from '../models/user-model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.scss'
})
export class UserDetailsComponent {
  @Input() user!: UserModel;
  @Input() readonly = false;

  @Output() offerChanged: EventEmitter<UserModel> = new EventEmitter<UserModel>();

  ngOnInit(): void {
    this.user = this.user || initialUserModel;
  }
}
