import { NgFor, NgIf } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { AuctionHubService } from '../../services/auction-hub.service';
import { Notification } from '../../../models/Notification';
import { NotificationService } from '../../services/notification.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-notification-drop-down',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './notification-drop-down.component.html',

  
})
export class NotificationDropDownComponent implements OnInit{
 notifications : Notification[] = [];
  isOpen = false; 
  userId  = 0;
  constructor(private auctionHub : AuctionHubService, private notificationService : NotificationService, private authService : AuthService) {}
  toggleDropdown() { this.isOpen = !this.isOpen;}

  async ngOnInit(): Promise<void> {
      this.userId = this.authService.getUserId();

      await this.getUserNotification(this.userId);
  }

  getUserNotification(id : number) {
    this.notificationService.getUserNotifications(id).subscribe(data => {
      this.notifications = data;
    })
  }
}
