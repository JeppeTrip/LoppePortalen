import { action, flow, flowResult, makeAutoObservable, observable } from "mobx";
import { MarketClient } from "../../stores/models";
import { RootStore } from "../RootStore";
import { Market as Dto } from "../../stores/models";
import { Market } from "../@DomainObjects/Market";


export class MarketStore {
    rootStore: RootStore
    transportLayer: MarketClient
    @observable markets: Market[]
    @observable selectedMarket: Market

    constructor(rootStore: RootStore, transportLayer: MarketClient) {
        makeAutoObservable(this)
        this.rootStore = rootStore;
        this.transportLayer = transportLayer
        this.markets = [] as Market[]
        this.selectedMarket = null
    }

    /**
     * Update market when new data is received from the server.
     */
    @action
    updateMarketFromServer(dto: Dto) {
        let market = this.markets.find(x => x.id === dto.marketId)
        if (!market) {
            market = new Market(this)
            this.markets.push(market)
        }
        market.updateFromServer(dto)
        return market
    }

    /**
    * Tries to fetch all organisations from the backend.
    * Force updates the entire organiser list.
    * TODO: Ensure that all the organisers in this list also removes it from the listening objects.
    */
    @flow
    *resolveMarketsAll() {
        console.log("resolve all markets")
        try {
            const result = yield this.transportLayer.getAllMarketInstances()
            result.forEach(element => {
                this.updateMarketFromServer(element)
            });
        }
        catch (error) {
            this.markets = []
        }
        finally {
            return this.markets
        }
    }

    @action
    resolveSelectedMarket(marketId: number) {
        flowResult(this.resolveMarketsAll())
            .then(
                action("fetchSuccess", result => {
                    const market = result.find(x => x.id === marketId);
                    if (!market) {
                        return null;
                    }
                    else {
                        market.select()
                        return market
                    }
                }),
                action("fetchFailed", error => {
                    return null;
                })
            )
    }

    @action
    createMarket() {
        const market = new Market(this);
        this.selectedMarket = market;
        return market;
    }
}