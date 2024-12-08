import { Component } from '@angular/core';
import {  FormGroup, Validators, ReactiveFormsModule, FormsModule, FormControl } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { CommonModule, NgIf } from '@angular/common';
import { User } from '../../../models/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
})


export class RegisterComponent {

  isLoading: boolean = false;
  errorMessage: string = '';
  selectedRole : string = "Viewer"
  constructor( private authService: AuthService, private router : Router) {
    
  }

   // Form Controls
   firstName = new FormControl('', Validators.required);
   lastName = new FormControl('', Validators.required);
   email = new FormControl('', [Validators.required, Validators.email]);
   password = new FormControl('', [Validators.required, Validators.minLength(6)]);
 
   // Form Group
   registerForm: FormGroup = new FormGroup({
     firstName: this.firstName,
     lastName: this.lastName,
     email: this.email,
     password: this.password,
   });

  
  async signUp(): Promise<void> {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched(); 
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    try {

      const newUser: User = {
        username: `${this.firstName.value} ${this.lastName.value}`, // Combine first and last name
        email: this.email.value as string,
        password: this.password.value as string,
        role: this.selectedRole,
        isActive: true
      };
      // console.log(newUser)
      const response = await this.authService.registerUser(newUser).toPromise();
      alert("User registered successfully")

      this.router.navigate(['/login']);

      
      // console.log('User registered successfully:', response);

      
    } catch (error) {
      this.errorMessage = 'Failed to register. Please try again.';
      console.error('Registration error:', error);
    } finally {
      this.isLoading = false;
    }
  }

  selectRole(role: string): void {
    this.selectedRole = role;
    console.log('Selected role:', this.selectedRole); // Logs the selected role
  }

 
}
