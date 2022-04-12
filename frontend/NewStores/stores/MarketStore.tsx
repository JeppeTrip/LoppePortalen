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
     * Update the market list when markets are received from the server 
     * For example called when the update function on an organiser Entity is called.
     * @param dtos the market dto
     * @returns the list of markets corresponding to the dtos put into it.
     */
    @action
    updateMarketstFromServer(dtos: Dto[]) {
        console.log("resolve markets from server")
        console.log(dtos)
        const markets = dtos.map(dto => {
            let market = this.markets.find(m => m.id === dto.marketId)
            if (!market) {
                market = new Market(this)
                market.update(dto);
                this.markets.push(market)
            } else {
                market.update(dto)
            }
            return (market);
        });
        return markets;
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
            console.log(result)
            this.updateMarketstFromServer(result.markets);
            return this.markets
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
}