import { action, makeAutoObservable, observable } from 'mobx';
import { RootStore } from '../../RootStore';

class MarketProfileUiStore {
    rootStore: RootStore;

    loadingMarket = true;
    marketLoadSuccess = false;
    errorLoadingMarket = false;

    loadingStalls = true;
    stallLoadSuccess = false;
    errorLoadingStalls = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    @action
    resetState() {
        this.loadingMarket = true;
        this.marketLoadSuccess = false;
        this.errorLoadingMarket = false;

        this.loadingStalls = true;
        this.stallLoadSuccess = false;
        this.errorLoadingStalls = false;
    }

    @action
    loadMarket(){
        this.loadingMarket = true;
        this.marketLoadSuccess = false;
        this.errorLoadingMarket = false;
    }

    @action
    hadMarketLoadingError() {
        this.loadingMarket = false;
        this.marketLoadSuccess = false;
        this.errorLoadingMarket = true;
    }

    @action
    marketLoadingSuccess(){
        this.loadingMarket = false;
        this.marketLoadSuccess = true;
        this.errorLoadingMarket = false;
    }
}

export { MarketProfileUiStore }