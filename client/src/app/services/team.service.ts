import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Team } from '../../models/team';
import { Player } from '../../models/Player';
import { Finance } from '../../models/Finance';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private url = "https://localhost:7061/api/Team";
  
  
  constructor( private httpClient : HttpClient) {}

  private token = localStorage.getItem("Token");
  private options = {
    headers: new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', `Bearer ${this.token}`)
  };
  
  getAllTeams() : Observable<Team[]> {
    return this.httpClient.get<Team[]>(this.url , this.options);
  }

  getTeamById(teamId : number): Observable<Team>{
   
    const urlPath = `${this.url}/${teamId}`;

    return this.httpClient.get<Team>(urlPath, this.options);
  } 

  getPlayersByTeamId(teamId: number) : Observable<Player[]>{
    const urlPath = `${this.url}/players/${teamId}`;
    return this.httpClient.get<Player[]>(urlPath, this.options);
  }

  getTeamFinances(teamId : number) : Observable<Finance[]>{
    const urlPath = `https://localhost:7061/api/Finance/team/${teamId}`;
    return this.httpClient.get<Finance[]>(urlPath, this.options);
  }
}
