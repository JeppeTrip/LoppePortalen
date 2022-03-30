export interface IMarket {
    id : number;
    organiserId : number;
    name : string;
    startDate : Date;
    endDate : Date;
    description : string;
    isCancelled : boolean;
}

export type MarketContextType = {
    markets: IMarket[];
    addMarket : (market : IMarket) => void;
    getMarketInstance : (id : number) => IMarket;
}