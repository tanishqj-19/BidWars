import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable,  catchError} from 'rxjs';
import { Auction } from '../../models/Auction';
import { Bid } from '../../models/Bid';

@Injectable({
  providedIn: 'root'
})
export class AuctionService {
  private token = localStorage.getItem("Token");
  private options = {
    headers: new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', `Bearer ${this.token}`)
  };
  private baseUrl = "https://localhost:7061/api/Auction";
  constructor(private httpClient : HttpClient) { }




  getAuctionsByStatus(status: string) : Observable<Auction[]> {
    const url = `${this.baseUrl}/${status}`;
    console.log(url)

    return this.httpClient.get<Auction[]>(url, this.options);
  }


  createAuction(newAuction : Auction) : Observable<Auction> {
    const url = `${this.baseUrl}/create`
    return this.httpClient.post<Auction>(url, newAuction, this.options)
    .pipe(
      catchError(error => {
        alert("Error creating an auction");
        console.log(error);
        throw error;
      })
    );
  }

  getHighestBid(auctionId : number, playerId : number) : Observable<Bid> {
    const url = `https://localhost:7061/api/Bid/highest/${auctionId}/${playerId}`
    return this.httpClient.get<Bid>(url, this.options);
  }

  
}
