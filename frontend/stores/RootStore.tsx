import { MarketStore } from './Markets/MarketStore'

export interface IRootStore {
    marketStore : MarketStore
}

export class RootStore implements IRootStore{
    marketStore : MarketStore;

    constructor(){
        this.marketStore = new MarketStore(this)
    }
}