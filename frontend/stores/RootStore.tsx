import { makeAutoObservable } from 'mobx';
import { MarketStore } from './Markets/MarketStore'
import { OrganiserStore } from './Organiser/OrganiserStore';
import { MarketFormUiStore } from './UiState/MarketForm/MarketFormUiStore';
import { UiStateStore } from './UiState/UiStateStore';
import { UserStore } from './User/UserStore';

export interface IRootStore {
    marketStore : MarketStore
    organiserStore : OrganiserStore
    uiStateStore : UiStateStore
    userStore : UserStore
    marketFormUiStore : MarketFormUiStore
}

export class RootStore implements IRootStore{
    marketStore : MarketStore;
    organiserStore : OrganiserStore;
    uiStateStore : UiStateStore;
    userStore : UserStore;
    marketFormUiStore: MarketFormUiStore;

    constructor(){
        makeAutoObservable(this)
        this.marketStore = new MarketStore(this);
        this.organiserStore = new OrganiserStore(this);
        this.uiStateStore = new UiStateStore(this);
        this.userStore = new UserStore(this);
        this.marketFormUiStore = new MarketFormUiStore(this);
    }
}