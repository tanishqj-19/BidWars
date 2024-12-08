export interface Auction{
    date : Date,
    sport : string,
    auctioneerId : number,
    startTime : Date,
    endTime : Date,
    status : string,
    playerId : number,
    auctionId? : number,
}