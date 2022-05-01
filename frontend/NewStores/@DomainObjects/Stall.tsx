import { action, makeAutoObservable, observable } from "mobx";
import { ModelState } from "../../@types/ModelState";
import { StallStore } from "../stores/StallStore";
import { StallType } from "./StallType";
import { GetBoothStallVM, GetFilteredBoothsStallVM, GetUsersBoothsStallVM, StallBaseVM as Dto } from '../../services/clients'
import { Market } from "./Market";
import { Booth } from "./Booth";



export class Stall {
    store: StallStore
    @observable state: symbol
    @observable id: number
    @observable type: StallType
    @observable market: Market
    @observable booth: Booth

    constructor(store: StallStore) {
        makeAutoObservable(this)
        this.store = store
        this.state = ModelState.NEW
    }

    @action
    updateFromServer(dto: Dto) {
        if (this.state != ModelState.UPDATING) {
            this.state = ModelState.UPDATING
            this.id = dto.id
            this.type = this.store.rootStore.stallTypeStore.updateStallTypeFromServer(dto.stallType);
            switch(dto.constructor.name)
            {
                case "GetUsersBoothsStallVM":
                    this.updateFromServerGetUsersBoothsStallVM(dto)
                    break;
                case "GetFilteredBoothsStallVM":
                    this.updateFromServerGetFilteredBoothsStallVM(dto)
                    break;
            }
            this.state = ModelState.IDLE
        }
        return this;
    }

    @action 
    updateFromServerGetUsersBoothsStallVM(dto : GetUsersBoothsStallVM)
    {
        const market = this.store.rootStore.marketStore.updateMarketFromServer(dto.market)
        if(this.market == null || this.market.id != market.id)
            this.market = market
    }

    @action
    updateFromServerGetBoothStallVM(dto : GetBoothStallVM)
    {
        const market = this.store.rootStore.marketStore.updateMarketFromServer(dto.market)
        if(this.market == null || this.market.id != market.id)
            this.market = market
    }

    @action
    private updateFromServerGetFilteredBoothsStallVM(dto : GetFilteredBoothsStallVM)
    {
        const market = this.store.rootStore.marketStore.updateMarketFromServer(dto.market)
        if(this.market == null || this.market.id != market.id)
            this.market = market
    }

    @action
    set setId(id: number) {
        this.id = id
    }

    @action
    set setType(stallType: StallType) {
        this.type = stallType
    }

    /**
    * Permanently delete this stall from server. 
    * Propagates the update to the related market and stalltype if they are loaded 
    * into memory.
    */
    @action
    delete() {
        if (this.state != ModelState.NEW) {
            this.state = ModelState.UPDATING
            this.store.transportLayer.deleteStall(this.id)
                .then(
                    action("deleteSuccess", res => {
                        if (res.succeeded) {
                            this.store.removeStall(this.id)
                        }
                        this.state = ModelState.ERROR
                    }),
                    action("deleteFailed", error => {
                        this.state = ModelState.ERROR
                    })
                )
        }
    }

}