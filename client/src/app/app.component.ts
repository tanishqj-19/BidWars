import { RouterOutlet, Router, NavigationEnd } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy{

  
  title = 'BidWars';

  showNavbar = true; // Controls the visibility of the navbar

  constructor(private router: Router) {}

  ngOnInit(): void {
 
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
      
        const hiddenRoutes = ['/login', '/register'];
        this.showNavbar = !hiddenRoutes.includes(event.urlAfterRedirects);
      }
    });
  }
  ngOnDestroy(): void {
      localStorage.removeItem("Token");

      
  }
  
  
}
