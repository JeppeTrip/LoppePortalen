import { action, makeAutoObservable, observable } from "mobx";
import { MarketClient } from "../../stores/models";
import { RootStore } from "../RootStore";
import { Market as Dto } from "../../stores/models";
import { Market } from "../@DomainObjects/Market";


export class MarketStore {
    rootStore : RootStore
    transportLayer : MarketClient
    @observable markets : Market[]

    constructor(rootStore : RootStore, transportLayer : MarketClient)
    {
        makeAutoObservable(this)
        this.rootStore = rootStore;
        this.transportLayer = transportLayer
        this.markets = [] as Market[]
    }

    /**
     * Update the market list when markets are received from the server 
     * For example called when the update function on an organiser Entity is called.
     * @param dtos the market dto
     * @returns the list of markets corresponding to the dtos put into it.
     */
    @action
    updateMarketstFromServer(dtos : Dto[])
    {
        const markets = dtos.map(dto => {
            let market = this.markets.find(m => m.id === dto.marketId)
            if(!market){
                market = new Market(this)
                market.update(dto);
                this.markets.push(market)
            } else {
                market.update(dto)
            }
            return(market);
        });
        return markets;
    }
}