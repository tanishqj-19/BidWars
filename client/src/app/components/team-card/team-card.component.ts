import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-team-card',
  standalone: true,
  imports: [],
  templateUrl: './team-card.component.html',

})
export class TeamCardComponent {
  @Input() team : any;

  imgpath: string = "";
  teamPath: string = "";
  

  ngOnChanges(): void {
    if (this.team) {
      this.imgpath = `assets/teams/${this.team.teamId}.jpg`; 
      this.teamPath = `/teams/${this.team.teamId}`
    }
  }

}
