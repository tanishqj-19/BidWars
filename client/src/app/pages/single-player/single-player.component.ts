import { Component, OnInit } from '@angular/core';
import { PerformanceReport } from '../../../models/PerformanceReport';
import { PlayerService } from '../../services/player.service';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe, DatePipe, NgFor, NgIf } from '@angular/common';
import { Player } from '../../../models/Player';
import { Contract } from '../../../models/Contract';
import { jsPDF } from 'jspdf';
import { BidWarsContractGenerator } from '../../Helper/BidWarsContractGenerator';
@Component({
  selector: 'app-single-player',
  standalone: true,
  imports: [NgFor, DatePipe, NgIf],
  templateUrl: './single-player.component.html',
  styles: ``
})
export class SinglePlayerComponent implements OnInit{
  Role = localStorage.getItem("Role")
  playerContract : Contract = {
    contractId : "Not Found",
    playerId : 0,
    startDate : new Date(),
    endDate: new Date(),
    salary : 0,
    bonuses : 0,
    details : "Not Found",

  }
  reports : PerformanceReport[] = []
  player : Player = {
    playerId : 0,
    name : "Not Found", 
    sport: "Not Found",
    age: 0,
    country :"Not Found",
    position: "Not Found",
    basePrice : 0,
    agentId : 0,
    status : "Not Found",
    teamId  : 0
  }
  private currPlayerId : number = 0;
  stats1 = "Runs Scored";
  stats2 = "Balls Played";
  stats3 = "Number of Wickets"
  stats4 = "Balls Thrown";

  constructor(private playerService : PlayerService, private route : ActivatedRoute){}

  currImgPath :string = "";

  ngOnInit() :void {
    this.route.paramMap.subscribe(params => {
      if(params.get('id')){
        this.currPlayerId = parseInt(params.get('id') ?? "0");
        this.currImgPath = `assets/players/${this.currPlayerId}.jpg`;
        this.getPlayer();
        this.getReports();
        
        
      }
      
    });

   this.getPlayerContract();
  }
  async getReports(){
    await this.playerService.getPerformanceReport(this.currPlayerId).subscribe(stats => {
      this.reports = stats;
    });
  }

  async getPlayerContract() : Promise<void> {
    await this.playerService.getPlayerContract(this.currPlayerId).subscribe(con =>{
      this.playerContract = con;
    })
  }

  async downloadContract() : Promise<void>{
    await BidWarsContractGenerator.generateContractPDF(this.player, this.playerContract)
  }
  // async downloadContract(): Promise<void> {
  //   if (!this.player || !this.playerContract) {
  //     console.error('Player or contract data is missing.');
  //     return;
  //   }
  //   var totalIncome = this.playerContract.salary + this.playerContract.bonuses;
  //   const doc = new jsPDF();
  
  //   // Helper Variables
  //   const marginLeft = 15;
  //   const pageWidth = doc.internal.pageSize.getWidth();
  //   let currentY = 20;

  //   // const logo = 'assets/cric'; // Replace with your base64-encoded logo
  //   // doc.addImage(logo, 'JPEG', 10, 10, 50, 20); // x, y, width, height

  
  //   // Add Title
  //   doc.setFont('Helvetica', 'bold');
  //   doc.setFontSize(18);
  //   doc.text('Bid Wars Sports Auction Legal Contract', pageWidth / 2, currentY, { align: 'center' });
  //   currentY += 10;
  
  //   // Add Subheading
  //   doc.setFontSize(14);
  //   doc.setFont('Helvetica', 'normal');
  //   doc.text('Player Contract Agreement', pageWidth / 2, currentY, { align: 'center' });
  //   currentY += 20;
  
  //   // Add Section Divider
  //   doc.setLineWidth(0.5);
  //   doc.line(marginLeft, currentY, pageWidth - marginLeft, currentY); // Horizontal line
  //   currentY += 10;
  
  //   // Add Player Details
  //   doc.setFontSize(12);
  //   doc.setFont('Helvetica', 'bold');
  //   doc.text('Player Details:', marginLeft, currentY);
  //   currentY += 10;
  //   doc.setFont('Helvetica', 'normal');
  //   doc.text(`Name: ${this.player.name}`, marginLeft, currentY);
  //   currentY += 8;
  //   doc.text(`Sport: ${this.player.sport}`, marginLeft, currentY);
  //   currentY += 8;
  //   doc.text(`Age: ${this.player.age}`, marginLeft, currentY);
  //   currentY += 8;
  //   doc.text(`Country: ${this.player.country}`, marginLeft, currentY);
  //   currentY += 8;
  //   doc.text(`Position: ${this.player.position}`, marginLeft, currentY);
  //   currentY += 15;
  
  //   // Add Financial Terms
  //   doc.setFont('Helvetica', 'bold');
  //   doc.text('Financial Terms:', marginLeft, currentY);
  //   currentY += 10;
    
  //   doc.setFont('Helvetica', 'normal');
  //   doc.text(`Base Salary: ${this.playerContract.salary}`, marginLeft, currentY);
  //   currentY += 8;
  //   doc.text(`Bonuses: ${this.playerContract.bonuses}`, marginLeft, currentY);
  //   currentY += 8;
  //   doc.text(`Total Income: $${totalIncome}`, marginLeft, currentY);
  //   currentY += 15;
  
  //   // Add Contract Period
  //   doc.setFont('Helvetica', 'bold');
  //   doc.text('Contract Period:', marginLeft, currentY);
  //   currentY += 10;
  //   doc.setFont('Helvetica', 'normal');
  //   doc.text(`Start Date: ${this.playerContract.startDate.toLocaleDateString()}`, marginLeft, currentY);
  //   currentY += 8;
  //   doc.text(`End Date: ${this.playerContract.endDate.toLocaleDateString()}`, marginLeft, currentY);
  //   currentY += 15;
  
  //   // Add Legal Note
  //   doc.setFont('Helvetica', 'italic');
  //   doc.text(
  //     'This contract is binding under the terms outlined and cannot be terminated without prior agreement by both parties.',
  //     marginLeft,
  //     currentY,
  //     { maxWidth: pageWidth - marginLeft * 2 }
  //   );
  //   currentY += 20;

  //   doc.text('Player Signature: _______________________', marginLeft, currentY + 20);
  //   doc.text('Authorized Signature: ___________________', marginLeft, currentY + 30);

  
  //   // Footer
  //   doc.setFont('Helvetica', 'bold');
  //   doc.setFontSize(10);
  //   doc.text('Bid Wars Auction Ltd.', pageWidth / 2, 280, { align: 'center' });
  
  //   // Save the PDF
  //   doc.save(`${this.player.name}_Contract.pdf`);
  // }
  
  

  async getPlayer(){
    await this.playerService.getPlayerById(this.currPlayerId).subscribe(p =>{
      this.player = p;
      // console.log(p.sport);
      if(p.sport == "Football"){
        this.stats1 = "Goals Scored";
        this.stats2 = "Assists";
        this.stats3 = "Passes Completed";
        this.stats4 = "Tackles Made";
      }
    })
  }
}
