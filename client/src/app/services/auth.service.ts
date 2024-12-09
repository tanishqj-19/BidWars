import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../models/User';
import { Observable, tap, catchError } from 'rxjs';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';





@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = "https://localhost:7061/api/Auth";
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
  private tokenExpirationTimeout: any;

  constructor(private httpClient: HttpClient, private router: Router) {}

  registerUser(newUser: User | any): Observable<User> {
    const url = `${this.baseUrl}/register`;
    return this.httpClient.post<User>(url, newUser, this.options).pipe(
      catchError(error => {
        console.error('Registration error:', error);
        throw error;
      })
    );
  }

  loginUser(email: string, password: string): Observable<any> {
    const url = `${this.baseUrl}/login`;
    const loginData = { email, password };

    return this.httpClient.post(url, loginData, this.options).pipe(
      tap((response: any) => {
        if (response && response.token) {
          this.setToken(response.token);
          this.scheduleAutoLogout(response.token);

          // Fetch user details after setting the token
          this.getUserByEmail(email).subscribe({
            next: (userDetails: User) => {
              this.setUserId(userDetails.userId?.toString() ?? "0");
              localStorage.setItem("Role", userDetails.role ?? "");
              localStorage.setItem("Username", userDetails.username ?? "");
              localStorage.setItem("Email", userDetails.email ?? "");
              console.log('Current User:', userDetails);
            },
            error: (error) => {
              alert('Error fetching user details');
              console.error('Error fetching user details:', error);
            }
          });
        }
      }),
      catchError((error) => {
        console.error('Login error:', error);
        throw error;
      })
    );
  }

  setToken(value: string): void {
    localStorage.setItem("Token", value);
  }

  setUserId(value: string): void {
    localStorage.setItem("USERID", value);
  }
  getUserId() {
    return parseInt(localStorage.getItem("USERID") ?? "0");
  }

  getToken(): string | null {
    return localStorage.getItem("Token");
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const decodedToken: any = jwtDecode(token);
    const currentTime = Math.floor(Date.now() / 1000); // Current time in seconds
    return decodedToken.exp > currentTime;
  }

  logout(): void {
    localStorage.removeItem("Token");
    localStorage.removeItem("USERID");
    localStorage.removeItem("Username");
    localStorage.removeItem("Email");
    localStorage.removeItem("Role");
    clearTimeout(this.tokenExpirationTimeout);
    this.router.navigate(['/login']);
  }

  getUserByEmail(email: string): Observable<User> {
    const url = `${this.baseUrl}/users?email=${email}`;
    return this.httpClient.get<User>(url, this.options).pipe(
      catchError((error) => {
        console.error('Error fetching user by email:', error);
        throw error;
      })
    );
  }

  private scheduleAutoLogout(token: string): void {
    const decodedToken: any = jwtDecode(token);
    const expirationTime = decodedToken.exp * 1000; // Convert to milliseconds
    const currentTime = Date.now();

    if (expirationTime > currentTime) {
      const timeoutDuration = expirationTime - currentTime;
      this.tokenExpirationTimeout = setTimeout(() => {
        this.logout();
      }, timeoutDuration);
    } else {
      this.logout(); // Token already expired
    }
  }

  autoLogin(): void {
    const token = this.getToken();
    if (token && this.isAuthenticated()) {
      this.scheduleAutoLogout(token);
    } else {
      this.logout();
    }
  }
}