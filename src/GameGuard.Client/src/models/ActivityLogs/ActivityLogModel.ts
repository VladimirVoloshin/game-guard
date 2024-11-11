export interface ActivityLogModel{
    id: number,
    playerId: number,
    playerUsername: string,
    action: string,
    timestamp: Date,
    isSuspicious: boolean,
    isReviewed: boolean
}