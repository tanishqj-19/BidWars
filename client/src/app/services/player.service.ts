import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Player } from '../../models/Player';
import { PerformanceReport } from '../../models/PerformanceReport';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contract } from '../../models/Contract';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  private url = "https://localhost:7061/api/Player";
  
  constructor(private authService: AuthService, private httpClient : HttpClient) { }

  private token = localStorage.getItem("Token");
  private options = {
    headers: new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', `Bearer ${this.token}`)
  };

  searchPlayers(searchInput : string) : Observable<Player[]> {

    const urlPath = `${this.url}/search/${searchInput}`
    return this.httpClient.get<Player[]>(urlPath, this.options);
  }

 
  getAllPlayers(): Observable<Player[]> {
    return this.httpClient.get<Player[]>(this.url, this.options);
  }

  getAvailablePlayers() : Observable<Player[]>{
    const path = `${this.url}/available`
    return this.httpClient.get<Player[]>(path, this.options);
  }

  getPerformanceReport(playerId : number) :Observable<PerformanceReport[]>{
    const urlPath = `${this.url}/reports/${playerId}`;

    return this.httpClient.get<PerformanceReport[]>(urlPath, this.options);
  }

  getPlayerById(playerId : number) : Observable<Player>{
    const urlPath = `${this.url}/${playerId}`;

    return this.httpClient.get<Player>(urlPath, this.options);
  }

  getPlayerBySport(currSport : string) : Observable<Player[]>{
    
    const urlPath = `${this.url}/sport?sport=${currSport}`;
    return this.httpClient.get<Player[]>(urlPath, this.options);
  }

  getPlayerContract(playerId : number) : Observable<Contract>{
    const urlPath = `${this.url}/contract/${playerId}`;

    return this.httpClient.get<Contract>(urlPath, this.options);
  }

  getAllPerformances() : Observable<PerformanceReport[]>{
    const path = `https://localhost:7061/api/PerformanceReport`
    return this.httpClient.get<PerformanceReport[]>(path, this.options);
  }

  
}
