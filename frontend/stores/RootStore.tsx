import { makeAutoObservable } from 'mobx';
import { MarketStore } from './Markets/MarketStore'
import { OrganiserStore } from './Organiser/OrganiserStore';
import { UiStateStore } from './UiState/UiStateStore';

export interface IRootStore {
    marketStore : MarketStore
    organiserStore : OrganiserStore
    uiStateStore : UiStateStore
}

export class RootStore implements IRootStore{
    marketStore : MarketStore;
    organiserStore : OrganiserStore;
    uiStateStore : UiStateStore;

    constructor(){
        makeAutoObservable(this)
        this.marketStore = new MarketStore(this);
        this.organiserStore = new OrganiserStore(this);
        this.uiStateStore = new UiStateStore(this);
    }
}