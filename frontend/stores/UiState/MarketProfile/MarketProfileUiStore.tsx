import { action, makeAutoObservable, observable } from 'mobx';
import { RootStore } from '../../RootStore';

class MarketProfileUiStore {
    rootStore: RootStore;

    @observable loadingMarket = true;
    @observable marketLoadSuccess = false;
    @observable errorLoadingMarket = false;

    @observable loadingStalls = true;
    @observable stallLoadSuccess = false;
    @observable errorLoadingStalls = false;

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

    @action
    loadStalls(){
        this.loadingStalls = true;
        this.stallLoadSuccess = false;
        this.errorLoadingStalls = false;
    }

    @action
    hadStallLoadingError() {
        this.loadingStalls = false;
        this.stallLoadSuccess = false;
        this.errorLoadingStalls = true;
    }

    @action
    stallLoadingSuccess(){
        this.loadingStalls = false;
        this.stallLoadSuccess = true;
        this.errorLoadingStalls = false;
    }
}

export { MarketProfileUiStore }