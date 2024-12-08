export interface Bid{
    bidId : number,
    auctionId : number,
    playerId : number,
    teamId : number,
    bidAmount : number,
    bidTime : Date,
    status : string
}