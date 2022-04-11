import { action, makeAutoObservable, observable } from 'mobx';
import * as React from 'react';
import { IMarket, Market } from '../../@types/Market';
import { Stall } from '../../@types/Stall';
import { MarketClient, StallClient, StallDto } from '../models';
import { RootStore } from '../RootStore';

class MarketStore {
    rootStore: RootStore;
    @observable markets: IMarket[];
    @observable selectedMarket: IMarket;
    @observable newMarket: Market 
    @observable editedMarket: IMarket

    //TODO: Move all UI state out of here.
    isLoading = true;
    hadLoadingError = false;

    isCancelling = false;
    hadCancellingError = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        //TODO: Something else I can do but magic numbers to signify that this is an uncreated market?
        this.rootStore = rootStore;
        this.newMarket = new Market(-1, -1, "", new Date(), new Date(), "", false, []);
        this.selectedMarket = new Market(
            -1,
            undefined,
            undefined,
            undefined,
            undefined,
            undefined,
            undefined,
            []);
        this.markets = [];
    }

    @action
    resetNewMarket() {
        this.newMarket = new Market(-1, -1, "", new Date(), new Date(), "", false, []);
    }

    @action
    loadMarkets() {
        this.setIsLoading(true);
        const client = new MarketClient()
        client.getAllMarketInstances().then(res => {
            var result = res.map(m => {
                return (
                    new Market(
                        m.marketId,
                        m.organiserId,
                        m.marketName,
                        new Date(m.startDate),
                        new Date(m.endDate),
                        m.description,
                        m.isCancelled,
                        []))
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
        //TODO: Research wether or not this is a good idea.
        console.log("Selected market.")
        var result = this.markets.find(market => market.id === marketId);
        console.log(result)
        if (result != null) {
            this.selectedMarket = result;
        }
        else {
            const client = new MarketClient();
            client.getMarketInstance(marketId + "")
                .then(res => {
                    this.selectedMarket = new Market(
                        res.marketId,
                        res.organiserId,
                        res.marketName,
                        new Date(res.startDate),
                        new Date(res.endDate),
                        res.description,
                        res.isCancelled,
                        []);
                }).catch(error => {
                    this.selectedMarket = new Market(
                        -1,
                        undefined,
                        undefined,
                        undefined,
                        undefined,
                        undefined,
                        undefined,
                        []);
                })
        }
    }

    @action
    addNewMarket() {
        this.rootStore.marketFormUiStore.beginSubmit()
        const client = new MarketClient();
        var stallDto: StallDto[] = this.newMarket.uniqueStalls().map<StallDto>(x => {
            return (
                {
                    name: x.type,
                    description: x.description,
                    count: this.newMarket.stallCount(x.type)
                }
            )
        });
        const request = {
            organiserId: this.newMarket.organiserId,
            marketName: this.newMarket.name,
            description: this.newMarket.description,
            startDate: this.newMarket.startDate,
            endDate: this.newMarket.endDate,
            stalls: stallDto
        }
        client.createMarket(request).then(res => {
            this.newMarket.id = res.marketId
            this.markets.push(this.newMarket); //TODO: Maybe send the entire market dto back, since this will give issues with the stall ids not being updated.
            this.rootStore.marketFormUiStore.submitSuccess()
        }).catch(error => {
            this.rootStore.marketFormUiStore.hadSubmissionError()
        })
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
    getFilteredMarkets(organiserId: number | null, hideCancelled: boolean, startDate: Date | null, endDate: Date | null) {
        this.setIsLoading(true);
        const client = new MarketClient()

        client.getFilteredMarketInstances(hideCancelled, organiserId, startDate, endDate).then(res => {
            var result = res.map(m => {
                return (
                    new Market(
                        m.marketId,
                        m.organiserId,
                        m.marketName,
                        new Date(m.startDate),
                        new Date(m.endDate),
                        m.description,
                        m.isCancelled,
                        []))
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
    setIsLoading(isLoading: boolean) {
        this.isLoading = isLoading
    }

    @action
    setHadLoadingError(hadLoadingError: boolean) {
        this.hadLoadingError = hadLoadingError
    }

    @action
    setMarkets(markets: IMarket[]) {
        this.markets = markets
    }

    @action
    fetchStallsForMarket(market: IMarket) {
        const client = new StallClient()

        client.getStallsForMarket(market.id)
            .then(res => {
                var result = res.map(stall => {
                    return (
                        new Stall(
                            stall.stallName,
                            stall.stallDescription,
                            stall.stallId))
                })
                market.setStalls(result);
            }).catch(error => {

            })
    }

    @action
    cancelMarket(market : IMarket)
    {
        const client = new MarketClient();
        client.cancelMarketInstance(market.id + "")
            .then(res => {
                if (res.marketId == market.id) {
                    this.selectedMarket.isCancelled = res.isCancelled;
                }
            })
            .catch(error => {

            });
    }

    @action
    setEditedMarket(market : IMarket)
    {
        this.editedMarket = market;
    }

    @action
    setSelectedMarketObject(market : IMarket)
    {
        this.selectedMarket = market
    }
}

export { MarketStore }