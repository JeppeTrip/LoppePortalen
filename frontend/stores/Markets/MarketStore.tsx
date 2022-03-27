import { action, makeAutoObservable } from 'mobx';
import * as React from 'react';
import { MarketContextType, IMarket } from '../../@types/Market';
import { MarketClient } from '../models';
import { RootStore } from '../RootStore';

class MarketStore {
    rootStore: RootStore;
    markets: IMarket[] = [];
    selectedMarket: IMarket = null;

    isLoading = true;
    hadLoadingError = false;

    isSubmitting = false;
    hadSubmissionError = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    loadMarkets() {
        this.isLoading = true;
        const client = new MarketClient()
        client.getAllMarketInstances().then(res => {
            var result = res.map(m => {
                return ({
                    id: m.marketId,
                    organiserId: m.organiserId,
                    name: m.marketName,
                    startDate: new Date(m.startDate),
                    endDate: new Date(m.endDate),
                    description: m.description
                })
            })
            this.markets = result;
            this.isLoading = false;
            this.hadLoadingError = false;
        }).catch(error => {
            this.hadLoadingError = true;
            this.isLoading = false;
        })
    }

    @action
    setSelectedMarket(market: IMarket) {
        this.selectedMarket = market;
    }

    getMarket(id: number): IMarket {
        return this.markets.find(market => market.id === id);
    }

    @action
    addNewMarket(market: IMarket) {
        market.id = Math.floor(Math.random() * 1000)
        this.markets.push(market);
        return market.id;
    }
}

export { MarketStore }