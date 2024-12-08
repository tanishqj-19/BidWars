import { Component, OnInit } from '@angular/core';
import { TeamService } from '../../services/team.service';
import { TeamCardComponent } from '../../components/team-card/team-card.component';
import { NgFor } from '@angular/common';
import { Team } from '../../../models/team';

@Component({
  selector: 'app-teams',
  standalone: true,
  imports: [TeamCardComponent, NgFor],
  templateUrl: './teams.component.html',
  styles: ``
})
export class TeamsComponent implements OnInit{
  teams : Team[] = []

  constructor(private teamService : TeamService){}


  ngOnInit() : void{
    this.teamService.getAllTeams().subscribe(data =>{
      this.teams = data;
      console.log(data)
    }, error => {
      console.log("Error fetching players:\n", error);
    })
  }

}
