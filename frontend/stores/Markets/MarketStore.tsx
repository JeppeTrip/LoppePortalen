import { action, makeAutoObservable } from 'mobx';
import * as React from 'react';
import { MarketContextType, IMarket } from '../../@types/Market';
import { MarketClient } from '../models';
import { RootStore } from '../RootStore';

class MarketStore {
    rootStore: RootStore;
    markets: IMarket[] = [];
    selectedMarket: IMarket = null;
    newMarket: IMarket

    isLoading = true;
    hadLoadingError = false;

    isSubmitting = false;
    hadSubmissionError = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        //TODO: Something else I can do but magic numbers to signify that this is an uncreated market?
        this.newMarket = {
            id: -1,
            organiserId: -1,
            name: "",
            startDate: new Date(),
            endDate: new Date(),
            description: ""
        }
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
        this.isSubmitting = true;
        const client = new MarketClient();
        const request = {
            organiserId: market.organiserId,
            marketName: market.name,
            description: market.description,
            startDate: market.startDate,
            endDate: market.endDate
        }
        this.newMarket = market;
        client.createMarket(request).then(res => {
            this.newMarket.id = res.marketId
            this.isSubmitting = false;
            this.hadSubmissionError = false;
        }).catch(error => {
            this.hadSubmissionError = true;
            this.isSubmitting = false;
        })

        market.id = Math.floor(Math.random() * 1000)
        this.markets.push(market);
        return market.id;
    }
}

export { MarketStore }