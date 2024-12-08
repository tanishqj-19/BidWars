import { Component, Input } from '@angular/core';
import { Player } from '../../../models/Player';

@Component({
  selector: 'app-player-card',
  standalone: true,
  imports: [],
  templateUrl: './player-card.component.html',
 
})
export class PlayerCardComponent {
  @Input() player: any = {};
  imgpath: string = "";
  urlPath : string = "";

  viewStatistics = false;

  ngOnChanges(): void {
    if (this.player) {
      this.urlPath = `players/${this.player.playerId}`
      this.imgpath = `assets/players/${this.player.playerId}.jpg`; 
    }
  }

  toggleCard(){
    this.viewStatistics = true;
  }
}
