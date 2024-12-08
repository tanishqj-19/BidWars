import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { NotificationDropDownComponent } from '../notification-drop-down/notification-drop-down.component';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, NotificationDropDownComponent],
  templateUrl: './navbar.component.html',
  
})
export class NavbarComponent implements OnInit{

  constructor (private authService : AuthService){}

  Username = "";
  Email = "";
  Role = "";
  isLoggedIn = this.authService.isAuthenticated();// Set to true if the user is logged in
  isMenuOpen = false;

  isDropdownOpen = false;
  ngOnInit(): void {
      this.isLoggedIn = this.authService.isAuthenticated();
      this.Username = localStorage.getItem("Username") ?? "";
      this.Email = localStorage.getItem("Email") ?? "";
      this.Role = localStorage.getItem("Role") ?? "";
  }
  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  viewProfile(): void {
    // // Navigate to the profile page
    // console.log('Navigating to profile page...');
  }

  async logout() : Promise<void>{
    await this.authService.logout();
    this.isLoggedIn = false;
    this.isDropdownOpen = false;
    
  }
  
}
