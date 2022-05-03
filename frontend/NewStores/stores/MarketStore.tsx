import { action, flow, makeAutoObservable, observable } from "mobx";
import { ModelState } from "../../@types/ModelState";
import { GetMarketInstanceQueryResponse, MarketBaseVM as Dto, MarketClient } from "../../services/clients";
import { Market } from "../@DomainObjects/Market";
import { RootStore } from "../RootStore";


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
    * Tries to fetch all markets from the backend.
    * Force updates the entire organiser list.
    */
    @action
    fetchAllMarkets() {
        this.transportLayer.getAllMarketInstances()
            .then(
                action("fetchSuccess", result => {
                    result.markets.forEach(dto => {
                        this.updateMarketFromServer(dto)
                    });
                }),
                action("fetchError", error => {
                    this.markets = []
                })
            )
    }

    /**
    * Tries to fetch filtered markets from the backend.
    * For now this resets the current markets instances and then fetches a whole new list.
    */
    @action
    fetchFilteredMarkets(organiserId?: number, isCancelled?: boolean, startDate?: Date, endDate?: Date, categories?: string[]) {
        this.markets = []
        this.transportLayer.getFilteredMarketInstances(isCancelled, organiserId, startDate, endDate, categories)
            .then(
                action("fetchSuccess", result => {
                    result.markets.forEach(dto => {
                        this.updateMarketFromServer(dto)
                    });
                }),
                action("fetchError", error => {
                    this.markets = []
                })
            )
    }

    /**
     * Find market instance.
     * Right now it just forces a fetch every time, we could in principal make two versions.
     * 1 that looks through the currently loaded markets and are content with that. 
     * and 1 that forces an update of the information.
     * @param marketId id of the market to find.
     */
    @action
    async resolveSelectedMarket(marketId: number, editing?: boolean) {
        this.transportLayer.getMarketInstance(marketId + "")
            .then(
                action("fetchSuccess", result => {
                    let market = this.updateMarketFromServer(result.market);
                    return market;
                }),
                action("fetchFailed", error => {
                    //do something with this.
                })
            )
    }

    /**
     * Fetches market from backend this is my workaround for resolve right now.
     * Should probably make an updated version that makes sure it gets all resolves and all that with as well.
     * @returns 
     */
    @flow
    *fetchMarket(marketId: number) {
        try {
            const res: GetMarketInstanceQueryResponse = yield this.transportLayer.getMarketInstance(marketId + "");
            const market = this.updateMarketFromServer(res.market);
            return market
        }
        catch(Error) {
            throw Error;
        }
    }

    @action
    createMarket() {
        const market = new Market(this);
        this.markets.push(market)
        this.selectedMarket = market;
        return market;
    }

    @action
    removeUnsavedMarketsFromList() {
        this.markets = this.markets.filter(x => x.state != ModelState.NEW);
    }

}