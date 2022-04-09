import { makeAutoObservable } from 'mobx';
import { MarketStore } from './Markets/MarketStore'
import { OrganiserStore } from './Organiser/OrganiserStore';
import { MarketFormUiStore } from './UiState/MarketForm/MarketFormUiStore';
import { MarketProfileUiStore } from './UiState/MarketProfile/MarketProfileUiStore';
import { StallFormUiStore } from './UiState/StallForm/StallFormUiStore';
import { UiStateStore } from './UiState/UiStateStore';
import { UserFormUiStore } from './UiState/UserForm/UserFormUiStore';
import { UserStore } from './User/UserStore';

export interface IRootStore {
    marketStore : MarketStore
    organiserStore : OrganiserStore
    uiStateStore : UiStateStore
    userStore : UserStore
    marketFormUiStore : MarketFormUiStore
    stallFormUiStore : StallFormUiStore
    marketProfileUiStore : MarketProfileUiStore
    userFormUiStore : UserFormUiStore
}

export class RootStore implements IRootStore{
    marketStore : MarketStore;
    organiserStore : OrganiserStore;
    uiStateStore : UiStateStore;
    userStore : UserStore;
    marketFormUiStore: MarketFormUiStore;
    stallFormUiStore : StallFormUiStore;
    marketProfileUiStore : MarketProfileUiStore
    userFormUiStore : UserFormUiStore

    constructor(){
        makeAutoObservable(this)
        this.marketStore = new MarketStore(this);
        this.organiserStore = new OrganiserStore(this);
        this.uiStateStore = new UiStateStore(this);
        this.userStore = new UserStore(this);
        this.marketFormUiStore = new MarketFormUiStore(this);
        this.stallFormUiStore = new StallFormUiStore(this);
        this.marketProfileUiStore = new MarketProfileUiStore(this);
        this.userFormUiStore = new UserFormUiStore(this);
    }
}