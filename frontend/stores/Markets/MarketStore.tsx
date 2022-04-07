import { action, makeAutoObservable } from 'mobx';
import * as React from 'react';
import { IMarket, Market } from '../../@types/Market';
import { MarketClient, StallDto } from '../models';
import { RootStore } from '../RootStore';

class MarketStore {
    rootStore: RootStore;
    markets: IMarket[] = [];
    selectedMarket: IMarket = null;
    newMarket: Market //reaction?????

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
    }

    @action
    resetNewMarket() {
        this.newMarket = new Market(-1, -1, "", new Date(), new Date(), "", false, []);
    }

    @action
    loadMarkets() {
        console.log("loadf markets")
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
                    isCancelled: m.isCancelled,
                    stalls: [] //TODO: Send stalls array back.
                })
            })
            this.setMarkets(result);
            this.setIsLoading(false);
            this.setHadLoadingError(false);
        }).catch(error => {
            console.log(error);
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
                    isCancelled: res.isCancelled,
                    stalls: [] //TODO: Send stalls array back.
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
    addNewMarket() {
        this.rootStore.marketFormUiStore.beginSubmit()
        const client = new MarketClient();
        var stallDto: StallDto[] = this.newMarket.uniqueStalls.map<StallDto>(x => {
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
        console.log(hideCancelled)
        console.log(startDate)
        console.log(endDate)
        this.setIsLoading(true);
        const client = new MarketClient()

        client.getFilteredMarketInstances(hideCancelled, organiserId, startDate, endDate).then(res => {
            console.log(res)
            var result = res.map(m => {
                return ({
                    id: m.marketId,
                    organiserId: m.organiserId,
                    name: m.marketName,
                    startDate: new Date(m.startDate),
                    endDate: new Date(m.endDate),
                    description: m.description,
                    isCancelled: m.isCancelled,
                    stalls: [] //Send stalls back.
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
}

export { MarketStore }