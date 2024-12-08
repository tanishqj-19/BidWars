import { Component, OnDestroy, OnInit } from '@angular/core';
import {CurrencyPipe, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Auction } from '../../../models/Auction';
import { CreateAuctionComponent } from '../../components/create-auction/create-auction.component';
import { AuctionCardComponent } from '../../components/auction-card/auction-card.component';
import { AuctionHubService } from '../../services/auction-hub.service';
import { AuctionService } from '../../services/auction.service';

@Component({
  selector: 'app-auction',
  standalone: true,
  imports: [CurrencyPipe, NgFor, FormsModule, NgIf, CreateAuctionComponent, AuctionCardComponent],
  templateUrl: './auction.component.html',
})



export class AuctionComponent implements OnInit, OnDestroy {
  
  Role = localStorage.getItem("Role");
  OngoingAuctions: Auction[] = [];
  ScheduledAuctions: Auction[] = [];
  CompletedAuctions: Auction[] = [];

  constructor(private auctionService: AuctionService, 
    private auctionHub: AuctionHubService) {}
    
  async ngOnInit(): Promise<void> {
    this.initializeAuctionData();
    await this.auctionHub.ensureConnection();
  }

  async ngOnDestroy(): Promise<void> {
    await this.auctionHub.destroy(); // Clean up SignalR connection
  }
 
  imgPath : string = "";
  private initializeAuctionData(): void {
    this.Role = localStorage.getItem("Role")
    this.getOngoingAuction();
    this.getScheduledAuction();
    this.getCompletedAuction();
  }

  

  async getOngoingAuction(): Promise<void> {
    this.auctionService.getAuctionsByStatus('ongoing').subscribe((aucs) => {
      this.OngoingAuctions = aucs;
    });
  }

  async getScheduledAuction(): Promise<void> {
    this.auctionService.getAuctionsByStatus('scheduled').subscribe((aucs) => {
      this.ScheduledAuctions = aucs;
    });
  }

  async getCompletedAuction(): Promise<void> {
    this.auctionService.getAuctionsByStatus('completed').subscribe((aucs) => {
      this.CompletedAuctions = aucs;
    });
  }

  
}



