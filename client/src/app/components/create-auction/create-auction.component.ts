import { Component, OnInit } from '@angular/core';
import { Auction } from '../../../models/Auction';
import { AuctionService } from '../../services/auction.service';
import { CommonModule, CurrencyPipe, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuctionHubService } from '../../services/auction-hub.service';
import { catchError, throwError, tap } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { PlayerService } from '../../services/player.service';
import { Player } from '../../../models/Player';
import { Router } from '@angular/router';



@Component({
  selector: 'app-create-auction',
  standalone: true,
  imports: [CurrencyPipe, NgFor, FormsModule, NgIf, CommonModule],
  templateUrl: './create-auction.component.html',
  styles: ``
})



export class CreateAuctionComponent implements OnInit {

  

  players : Player[] = [];
  private currUserId : number = 0;
  UserRole = localStorage.getItem("Role");
  auction: Auction = {
    date: new Date(),
    sport: '',
    auctioneerId: this.currUserId,
    startTime: new Date(),
    endTime: new Date(),
    status: 'Scheduled', 
    playerId: 0,
  };

  showForm = false;
  isSubmitting = false;

 
  errorMessage: string = "";

  constructor(
    private router : Router,
    private auctionService: AuctionService, 
    private signalRHub: AuctionHubService,
    private authService: AuthService , private playerServices : PlayerService
  ) {}

  async ngOnInit():Promise<void> {
    
    
    this.currUserId = await this.authService.getUserId();
    this.auction.auctioneerId = this.currUserId;

    await this.getAvailablePlayers();

  }

  

  onPlayerChange(event: Event): void {
    const selectedPlayerId = (event.target as HTMLSelectElement).value;
    this.auction.playerId = +selectedPlayerId; // Update playerId in auction object
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    this.resetForm();
  }

  resetForm(): void {
    this.auction = {
      date: new Date(Date.now()),
      sport: '',
      auctioneerId: this.auction.auctioneerId, 
      startTime: new Date(),
      endTime: new Date(),
      status: 'Pending',
      playerId: this.players[0].playerId,
    };
    this.errorMessage = "";
    this.isSubmitting = false;
  }

  validateAuction(): boolean {
    // Basic validation
    var currTime = Date.now;
    if (!this.auction.sport) {
      this.errorMessage = 'Please select a sport';
      return false;
    }
    if(this.auction.startTime >= this.auction.endTime){
      this.errorMessage = 'Start time should be less than auction time'
      return false;
    }
    if(this.auction.status == "Completed"){
      this.errorMessage = 'Cannot create expired auctions!'
      return false;
    }

    if (!this.auction.playerId) {
      this.errorMessage = 'Please select a player';
      return false;
    }

    if (this.auction.startTime >= this.auction.endTime) {
      this.errorMessage = 'End time must be after start time';
      return false;
    }

    return true;
  }

  getAvailablePlayers(): void {
    this.playerServices.getAvailablePlayers().subscribe(data => {
      // data.forEach(player => this.players.set(player.name, player));
      this.players = data;
    });
  }

  async createAuction(): Promise<void> {
   
    this.errorMessage = "";
    
   
   
    if (!this.validateAuction()) {
      alert(this.errorMessage);
      return;
    }

    if (this.isSubmitting) {
      return;
    }

    this.isSubmitting = true;
    await this.signalRHub.createAuction(this.auction).pipe(
      tap(auc => {
        console.log(auc);

        this.signalRHub.joinAuction(auc.auctionId ?? 0);
        
        
        this.showForm = false;
        this.resetForm();
      }),
      catchError(error => {
        console.error('Auction Creation Failed', error);
        
        this.errorMessage = error.message || 'Failed to create auction. Please try again.';
      
        this.isSubmitting = false;
        return throwError(error);
      })
    ).subscribe();
   
    await setTimeout(()=> {}, 2000)
   await this.router.navigate(['/auction']).then(() => {
      location.reload(); 
    });
  }

  
}