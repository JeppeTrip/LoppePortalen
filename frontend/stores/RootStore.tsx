import { makeAutoObservable } from 'mobx';
import { MarketStore } from './Markets/MarketStore'
import { OrganiserStore } from './Organiser/OrganiserStore';

export interface IRootStore {
    marketStore : MarketStore
    organiserStore : OrganiserStore
}

export class RootStore implements IRootStore{
    marketStore : MarketStore;
    organiserStore : OrganiserStore;

    constructor(){
        makeAutoObservable(this)
        this.marketStore = new MarketStore(this)
        this.organiserStore = new OrganiserStore(this)
    }
}