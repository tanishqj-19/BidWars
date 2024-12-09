import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from '../../services/team.service';
import { Team } from '../../../models/team';
import { CurrencyPipe, DatePipe, NgFor, NgIf } from '@angular/common';
import { PlayerCardComponent } from '../../components/player-card/player-card.component';
import { Finance } from '../../../models/Finance';
@Component({
  selector: 'app-single-team',
  standalone: true,
  imports: [CurrencyPipe, NgFor, NgIf, PlayerCardComponent, DatePipe],
  templateUrl: './single-team.component.html'
})
export class SingleTeamComponent implements OnInit{
  finances : Finance[] = [];
  showSquad = true;
  showFinance = false;
  constructor(private route : ActivatedRoute, private teamService : TeamService){}
  currId : number = 0;
  players : any[] = [];
  currTeam : Team = {
    teamId: -1,
    name : "Not Found",
    managerId: -1,
    sport: "Not Found",
    budget: -1,
    region: "Not Found",
    totalExpenditure: -1
  }

  currImgPath : string = "";
  ngOnInit(){
    this.route.paramMap.subscribe(params => {
      if(params.get('id')){
        this.currId = parseInt(params.get('id') ?? "0");
        this.currImgPath = `assets/teams/${this.currId}.jpg`;
        this.getTeamDetails();
        this.getTeamPlayers();
        this.getTeamFinance();

      } // Assuming your route has a parameter named 'id'
      // console.log(this.currId); 
    });
  }

  async getTeamPlayers(){
    await this.teamService.getPlayersByTeamId(this.currId).subscribe(players => {
      this.players = players;
    }
  );
  }

  async getTeamDetails() {
    await this.teamService.getTeamById(this.currId).subscribe(team =>{
      this.currTeam = team;
    });
  }
  async getTeamFinance(){
    // this.showFinances = true;
    await this.teamService.getTeamFinances(this.currId).subscribe(data => {
      this.finances = data;
    })
  }

  toggleView(view: string) {
    if (view === 'squad') {
      this.showSquad = true;
      this.showFinance = false;
    } else if (view === 'finance') {
      this.showSquad = false;
      this.showFinance = true;
    }
  }
  
}


