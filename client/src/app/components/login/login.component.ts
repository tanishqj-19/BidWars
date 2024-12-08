import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators, FormGroup } from '@angular/forms';
import { User } from '../../../models/User';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
})
export class LoginComponent {

  isPasswordVisible : boolean = false;
  loginError: string | null = null;
  isLoading: boolean = false;
  
  constructor(private authService : AuthService, private router : Router) {}


  // email Control
  email = new FormControl("", [
    Validators.required,
    Validators.email
  ]);

  // password Control
  password = new FormControl("", [
    Validators.required,
    Validators.minLength(6)
  ]);

  loginForm = new FormGroup({
    email: this.email,
    password: this.password
  });



  async login(): Promise<void> {
    this.isLoading = true;

    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      if (email && password) {
        await this.authService.loginUser(email, password).subscribe({
          next: () => {
            alert("Successfully Logged In!")
            this.router.navigate(['/']);
            this.loginError = null; // Clear error on success
            this.isLoading = false;
          },
          error: (err) => {
            console.error('Login failed:', err);
            this.isLoading = false;
            this.loginError = err?.error?.message || 'Invalid email or password'; // Display a user-friendly error
          }
        });
      }
    } else {
      this.loginError = 'Please fill in all required fields correctly.';
      this.isLoading = false;
    }
  }

  togglePassword(): void {
    this.isPasswordVisible = !this.isPasswordVisible;
  }


}
