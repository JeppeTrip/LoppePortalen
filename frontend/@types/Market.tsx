export interface IMarket {
    id : number;
    name : string;
    startDate : Date;
    endDate : Date;
    description : string;
}

export type MarketContextType = {
    markets: IMarket[];
    addMarket : (market : IMarket) => void;
}