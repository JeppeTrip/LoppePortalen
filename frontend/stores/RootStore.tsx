import { makeAutoObservable } from 'mobx';
import { AuthStore } from './Auth/AuthStore';
import { MarketStore } from './Markets/MarketStore'
import { OrganiserStore } from './Organiser/OrganiserStore';
import { MarketFormUiStore } from './UiState/MarketForm/MarketFormUiStore';
import { MarketProfileUiStore } from './UiState/MarketProfile/MarketProfileUiStore';
import { StallFormUiStore } from './UiState/StallForm/StallFormUiStore';
import { UiStateStore } from './UiState/UiStateStore';
import { UserFormUiStore } from './UiState/UserForm/UserFormUiStore';
import { UserProfileUiStore } from './UiState/UserProfile/UserProfileUiStore';
import { UserStore } from './User/UserStore';

export interface IRootStore {
    authStore : AuthStore
    marketStore : MarketStore
    organiserStore : OrganiserStore
    uiStateStore : UiStateStore
    userStore : UserStore
    marketFormUiStore : MarketFormUiStore
    stallFormUiStore : StallFormUiStore
    marketProfileUiStore : MarketProfileUiStore
    userFormUiStore : UserFormUiStore
    userProfileUiStore : UserProfileUiStore
}

export class RootStore implements IRootStore{
    authStore : AuthStore;
    marketStore : MarketStore;
    organiserStore : OrganiserStore;
    uiStateStore : UiStateStore;
    userStore : UserStore;
    marketFormUiStore: MarketFormUiStore;
    stallFormUiStore : StallFormUiStore;
    marketProfileUiStore : MarketProfileUiStore
    userFormUiStore : UserFormUiStore
    userProfileUiStore : UserProfileUiStore

    constructor(){
        makeAutoObservable(this)
        this.authStore = new AuthStore(this);
        this.marketStore = new MarketStore(this);
        this.organiserStore = new OrganiserStore(this);
        this.uiStateStore = new UiStateStore(this);
        this.userStore = new UserStore(this);
        this.marketFormUiStore = new MarketFormUiStore(this);
        this.stallFormUiStore = new StallFormUiStore(this);
        this.marketProfileUiStore = new MarketProfileUiStore(this);
        this.userFormUiStore = new UserFormUiStore(this);
        this.userProfileUiStore = new UserProfileUiStore(this);
    }
}