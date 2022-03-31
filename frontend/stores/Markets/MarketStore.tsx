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

    isCancelling = false;
    hadCancellingError = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        //TODO: Something else I can do but magic numbers to signify that this is an uncreated market?
        this.newMarket = {
            id: -1,
            organiserId: -1,
            name: "",
            startDate: new Date(),
            endDate: new Date(),
            description: "",
            isCancelled: false
        }
        this.rootStore = rootStore;
    }

    @action
    loadMarkets() {
        this.setIsLoading(true);
        const client = new MarketClient()
        client.getAllMarketInstances().then(res => {
            var result = res.map(m => {
                return ({
                    id: m.marketId,
                    organiserId: m.organiserId,
                    name: m.marketName,
                    startDate: new Date(m.startDate),
                    endDate: new Date(m.endDate),
                    description: m.description,
                    isCancelled: m.isCancelled
                })
            })
            this.setMarkets(result);
            this.setIsLoading(false);
            this.setHadLoadingError(false);
        }).catch(error => {
            this.setHadLoadingError(true);
            this.setIsLoading(false);
        })
    }

    @action
    setSelectedMarket(marketId: number) {
        this.isLoading = true;
        var result = this.markets.find(market => market.id === marketId);
        if (result === undefined) {
            const client = new MarketClient();
            client.getMarketInstance(marketId + "").then(res => {
                this.selectedMarket =
                {
                    id: res.marketId,
                    organiserId: res.organiserId,
                    name: res.marketName,
                    startDate: new Date(res.startDate),
                    endDate: new Date(res.endDate),
                    description: res.description,
                    isCancelled: res.isCancelled
                }
                this.isLoading = false;
                this.hadLoadingError = false;
            }).catch(error => {
                this.isLoading = false;
                this.hadLoadingError = true;
            })
        } else {
            this.selectedMarket = result;
            this.isLoading = false
        }
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

    @action
    cancelSelectedMarket() {
        this.hadCancellingError = false;

        if (this.selectedMarket == null) {
            //throw an exception here.
        } else {
            this.isCancelling = true;
            const client = new MarketClient();
            client.cancelMarketInstance(this.selectedMarket.id + "")
                .then(res => {
                    if (res.marketId == this.selectedMarket.id) {
                        this.selectedMarket.isCancelled = res.isCancelled;
                    }
                    this.isCancelling = false
                })
                .catch(error => {
                    this.hadCancellingError = true;
                    this.isCancelling = false;
                });
        }
    }

    @action
    getFilteredMarkets(organiserId: number | null, hideCancelled : boolean, startDate : Date | null, endDate : Date | null) {
        console.log (hideCancelled)
        console.log(startDate)
        console.log(endDate)
        this.setIsLoading(true);
        const client = new MarketClient()

        client.getFilteredMarketInstances(hideCancelled, startDate, endDate).then(res => {
            console.log(res)
            var result = res.map(m => {
                return ({
                    id: m.marketId,
                    organiserId: m.organiserId,
                    name: m.marketName,
                    startDate: new Date(m.startDate),
                    endDate: new Date(m.endDate),
                    description: m.description,
                    isCancelled: m.isCancelled
                })
            })
            this.setMarkets(result);
            this.setIsLoading(false);
            this.setHadLoadingError(false);
        }).catch(error => {
            this.setHadLoadingError(true);
            this.setIsLoading(false);
        })
    }

    @action
    setIsLoading(isLoading : boolean) {
        this.isLoading = isLoading
    }

    @action
    setHadLoadingError(hadLoadingError : boolean)
    {
        this.hadLoadingError = hadLoadingError
    }

    @action
    setMarkets(markets : IMarket[])
    {
        this.markets = markets
    }
    
}

export { MarketStore }