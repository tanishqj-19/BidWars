import * as signalR from '@microsoft/signalr';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable, Subject, from, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Auction } from '../../models/Auction';
import { Notification } from '../../models/Notification';
@Injectable({
  providedIn: 'root'
})
export class AuctionHubService  {

  notifications : Notification[] = [];
  private hubConnection: signalR.HubConnection | undefined;
  private isConnected = false;
  // Subjects for bid-related events
  private bidReceivedSubject = new Subject<Notification[]>();
  public bidReceived$: Observable<Notification[]> = this.bidReceivedSubject.asObservable();

  // Connection status
  private connectionStatusSubject = new BehaviorSubject<boolean>(false);
  public connectionStatus$: Observable<boolean> = this.connectionStatusSubject.asObservable();

  constructor() {
    this.initializeConnection();
  }
  // ngOnInit(){
  //   this.initializeConnection();
  // }

  async initializeConnection(): Promise<void> {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/auctionHub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect([0, 1000, 5000])
      .build();

    // // Set up listeners
    await this.setupListeners();

    // Start connection
    await this.startConnection();
  }

  private setupListeners(): void {
    if (!this.hubConnection) return;

    // Listen for bid updates
    this.hubConnection.on('ReceiveBid', (bidData) => {
      const newNotification = bidData;
      this.notifications = [newNotification, ...this.notifications]; // Update notifications array
      this.bidReceivedSubject.next(this.notifications); // Notify subscribers
  });

    // Connection events
    this.hubConnection.onclose(() => {
      console.log('SignalR connection closed');
      this.connectionStatusSubject.next(false);
    });

    this.hubConnection.onreconnecting(() => {
      console.log('SignalR reconnecting');
      this.connectionStatusSubject.next(false);
    });

    this.hubConnection.onreconnected(() => {
      console.log('SignalR reconnected');
      this.connectionStatusSubject.next(true);
    });
  }

  ensureConnection() {
    if (!this.isConnected) {
      this.startConnection();
    }
  }

  private startConnection(): void {
    if (!this.hubConnection ) return;

    from(this.hubConnection.start()).pipe(
      tap(() => {
        console.log('SignalR Connected');
        this.isConnected = true;
      }),
      catchError((err) => {
        console.error('Connection error', err);
        this.isConnected = false;
        return of(null);
      })
    ).subscribe();
  }

  public joinAuction(auctionId: number): Observable<any> {
    return this.invokeHubMethod('JoinAuctionRoom', auctionId);
  }

  public placeBid(auctionId: number, playerId: number, userId: number, bidAmount: number): Observable<any> {
    return this.invokeHubMethod('PlaceBid', auctionId, playerId, userId, bidAmount);
  }

  public createAuction(newAuction : Auction) : Observable<any>{
    var auctionDto = {
      Date : newAuction.date,
      Sport : newAuction.sport,
      AuctioneerId : newAuction.auctioneerId,
      StartTime : newAuction.startTime,
      EndTime : newAuction.endTime,
      Status : newAuction.status,
      PlayerId : newAuction.playerId,
    }

    return this.invokeHubMethod('CreateAuction', auctionDto);
  }

  public leaveAuctionRoom(auctionId: number): Observable<any> {
    return this.invokeHubMethod('LeaveAuctionGroup', auctionId);
  }

  private invokeHubMethod(methodName: string, ...args: any[]): Observable<any> {
    if (!this.hubConnection || this.hubConnection.state !== signalR.HubConnectionState.Connected) {
      return new Observable(observer => {
        observer.error('SignalR connection not established');
      });
    }

    return from(this.hubConnection.invoke(methodName, ...args)).pipe(
      catchError((error) => {
        console.error(`Error invoking ${methodName}:`, error);
        return new Observable(observer => {
          observer.error(error);
        });
      })
    );
  }

  public stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop()
        .then(() => console.log('SignalR connection stopped'))
        .catch(err => console.error('Error stopping connection', err));
    }
  }
  get connectionStatus(): boolean {
    return this.isConnected;
  }
  // Cleanup method (call this when component is destroyed)
  public destroy(): void {
    this.stopConnection();
    this.bidReceivedSubject.complete();
    this.connectionStatusSubject.complete();
  }
}