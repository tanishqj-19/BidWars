import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Auction } from '../../../models/Auction';
import { PlayerService } from '../../services/player.service';
import { Player } from '../../../models/Player';
import { CurrencyPipe, DatePipe, NgIf } from '@angular/common';
import { AuctionHubService } from '../../services/auction-hub.service';
import { AuthService } from '../../services/auth.service';
import { User } from '../../../models/User';
import { interval, Subject, Subscription } from 'rxjs';
import { OnInit, OnDestroy } from '@angular/core';
import { AuctionService } from '../../services/auction.service';
import { Bid } from '../../../models/Bid';
import { differenceInMilliseconds } from 'date-fns';
import { TeamService } from '../../services/team.service';

@Component({
  selector: 'app-auction-card',
  standalone: true,
  imports: [DatePipe, CurrencyPipe, NgIf],
  templateUrl: './auction-card.component.html',

})
export class AuctionCardComponent implements OnInit, OnDestroy{
  @Input() auction!: Auction;
  UserRole = localStorage.getItem("Role");

  player: Player = {
    name: '',
    sport: '',
    age: 0,
    country: '',
    position: '',
    basePrice: 0,
    playerId: 0,
    agentId: 0,
    status: '',
    teamId: 0,
  };
  teamName : string = '';

  timeRemaining: string = '';
  displayText: string = "Auction Ends In ";
  private timerSubscription!: Subscription;

  private increaseBid: number = 1000;
  currentBidAmount: number = 0;
  // currUser: User;
  imgPath: string = '';
  currUserId : number = 0;
  constructor(
    private playerService: PlayerService,
    private signalRHub: AuctionHubService,
    private authService: AuthService,
    private auctionService : AuctionService,
    private teamService  : TeamService
  ) {
    this.currUserId = this.authService.getUserId();
  }

  async ngOnInit(): Promise<void> {
    await this.initializeComponent();
    await this.getHighestBid();
    await this.getHighestBidder();
    // await this.signalRHub.ensureConnection();
    if (this.auction && this.auction.status === 'Ongoing') {
      this.joinAuctionRoom(); 
      
    }
    await this.startLiveCountdown();
    // this.subscribeToBidUpdates();
  }
 

  private async initializeComponent(): Promise<void> {
    if (this.auction) {
      this.imgPath = `assets/players/${this.auction.playerId}.jpg`;
      await this.fetchPlayerDetails();
      // this.getHighestBid(); 
    }
  }

  private fetchPlayerDetails(): Promise<void> {
    return new Promise((resolve) => {
      this.playerService.getPlayerById(this.auction.playerId).subscribe({
        next: (player) => {
          this.player = player;
          resolve();
        },
        error: (err) => {
          console.error('Error fetching player details:', err);
          resolve(); // Resolve even if there's an error to avoid blocking
        },
      });
    });
  }

  private joinAuctionRoom(): void {
    this.signalRHub.joinAuction(this.auction.auctionId ?? 0).subscribe({
      next: () => console.log(`Joined auction room: ${this.auction.auctionId}`),
      error: (err) => console.error('Failed to join auction room:', err),
    });
  }

  // private subscribeToBidUpdates(): void {
  //   this.signalRHub.bidReceived$.subscribe((bidData) => {
  //     if (bidData.auctionId === this.auction.auctionId) {
  //       this.currentBidAmount = bidData.newBidAmount;
  //       console.log(`New bid for auction ${this.auction.auctionId}: ${bidData.newBidAmount}`);
  //     }
  //   });
  // }
  
  highestbid : Bid = {
    auctionId : 0,
    teamId : 0,
    playerId : 0,
    bidId : 0,
    status : "Not Found",
    bidTime : new Date(),
    bidAmount : 0


  }
  private getHighestBid(): void {
    this.auctionService.getHighestBid(this.auction.auctionId ?? 0, this.player.playerId).subscribe({
      next: (bid) => {
        if (bid) {
          this.highestbid = bid;
          this.currentBidAmount = this.highestbid.bidAmount;
        } else {
          this.currentBidAmount = this.player.basePrice;
        }
      },
      error: (err) => {
        console.error('Error fetching bid details:', err);
        // Fallback to base price in case of error
        this.currentBidAmount = this.player.basePrice;
      },
    });
  }
  placeBid(): any {
    const nextValidBidAmount = this.currentBidAmount + this.increaseBid;

    if (this.currUserId <= 0) {
      console.warn('User not logged in. Bid cannot be placed.');
      return;
    }

    this.signalRHub.placeBid(
      this.auction.auctionId ?? 0,
      this.auction.playerId,
      this.currUserId,
      nextValidBidAmount
    ).subscribe({
      next: () => { this.currentBidAmount = nextValidBidAmount; console.log('Bid placed successfully')
       this.getHighestBid();
       this.getHighestBidder();
       },
      error: (error) => console.error('Failed to place bid:', error),
    });
  }

  getHighestBidder() : void{

    if(this.highestbid.teamId > 0){
      this.teamService.getTeamById(this.highestbid.teamId).subscribe((t) =>{
        this.teamName = t.name;
      });
    }
    
  }

  startLiveCountdown(): void {
    this.timerSubscription = interval(1000).subscribe(() => {
      const now = new Date();
      const startTime = new Date(this.auction.startTime);
      const endTime = new Date(this.auction.endTime);

      if (this.auction.status === 'Scheduled' && now < startTime) {
        this.displayText ="Auction Starts In "
        this.timeRemaining = this.formatTimeDifference(differenceInMilliseconds(startTime, now));
      } else if (this.auction.status === 'Ongoing' && now < endTime) {
        this.displayText = "Auction Ends In "
        this.timeRemaining = this.formatTimeDifference(differenceInMilliseconds(endTime, now));
      } else {
        this.displayText = "";
        this.timeRemaining = 'Auction ended';
      }
    });
  }

  formatTimeDifference(ms: number): string {
    const totalSeconds = Math.floor(ms / 1000);
    const hours = Math.floor(totalSeconds / 3600);
    const minutes = Math.floor((totalSeconds % 3600) / 60);
    const seconds = totalSeconds % 60;

    return `${this.padNumber(hours)}:${this.padNumber(minutes)}:${this.padNumber(seconds)}`;
  }

  padNumber(num: number): string {
    return num < 10 ? '0' + num : num.toString();
  }

   ngOnDestroy(): void {
    
    if (this.auction?.auctionId) {
       this.signalRHub.leaveAuctionRoom(this.auction.auctionId).subscribe({
        next: () => console.log(`Left auction room: ${this.auction.auctionId}`),
        error: (err) => console.error('Failed to leave auction room:', err),
      });
    }
    if (this.timerSubscription) {
       this.timerSubscription.unsubscribe();
    }
  }
}

