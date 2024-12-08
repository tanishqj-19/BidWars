import { Component, OnInit } from '@angular/core';
import { Player } from '../../../models/Player';
import { PlayerService } from '../../services/player.service';
import { CommonModule, NgFor } from '@angular/common';
import { PlayerCardComponent } from '../../components/player-card/player-card.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-view-players',
  standalone: true,
  imports: [CommonModule, NgFor, PlayerCardComponent, FormsModule],
  templateUrl: './view-players.component.html',
  styles: ``
})
export class ViewPlayersComponent implements OnInit{
  players : any[] = [];
  Sport : string = "";
  searchText : string = "";
  constructor(private playerService : PlayerService){}

  ngOnInit() : void{
    this.playerService.getAllPlayers().subscribe(data =>{
      this.players = data;
      // console.log(data)
    }, error => {
      console.log("Error fetching players:\n", error);
    })
  }
  onSportChange(event: Event): void {
    const selectedSport = (event.target as HTMLSelectElement).value;
    if (selectedSport) {
      this.Sport = selectedSport;
      this.getPlayersBySport();
    }
  }
  getSearchedPlayers() : void {
    this.playerService.searchPlayers(this.searchText).subscribe(data =>{
      this.players = data;
    })
  }
  getPlayersBySport() : void {
    this.playerService.getPlayerBySport(this.Sport).subscribe(data=>{
      this.players = data;
    }, error  => {
      console.log("Error fetching players:\n", error);
    })
  };

  search(){
    this.getSearchedPlayers();
  }



  
}
