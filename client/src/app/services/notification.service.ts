import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Notification } from '../../models/Notification';
@Injectable({
  providedIn: 'root'
})


export class NotificationService {
  private token = localStorage.getItem("Token");
  private options = {
    headers: new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', `Bearer ${this.token}`)
  };

  private readonly url = "https://localhost:7061/api/Notification";

  
  constructor(private httpClient : HttpClient) { }
  // getAllPlayers(): Observable<Player[]> {
  //   return this.httpClient.get<Player[]>(this.url, this.options);
  // }
  getUserNotifications(userId : number) : Observable<Notification[]>{
    const path = `${this.url}/${userId}`

    return this.httpClient.get<Notification[]>(path, this.options);
  }
}
