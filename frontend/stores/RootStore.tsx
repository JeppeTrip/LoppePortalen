import { makeAutoObservable } from 'mobx';
import { MarketStore } from './Markets/MarketStore'

export interface IRootStore {
    marketStore : MarketStore
}

export class RootStore implements IRootStore{
    marketStore : MarketStore;

    constructor(){
        makeAutoObservable(this)
        this.marketStore = new MarketStore(this)
    }
}