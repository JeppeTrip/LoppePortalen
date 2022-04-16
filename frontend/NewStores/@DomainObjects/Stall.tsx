import { action, makeAutoObservable, observable } from "mobx";
import { ModelState } from "../../@types/ModelState";
import { StallStore } from "../stores/StallStore";
import { StallType } from "./StallType";
import { Stall as Dto } from '../../services/clients'
import { Market } from "./Market";



export class Stall {
    store: StallStore
    @observable state: symbol
    @observable id: number
    @observable type: StallType
    @observable market: Market

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
            if(dto.market.stalls == null)
                dto.market.stalls = [dto]
            this.market = this.store.rootStore.marketStore.updateMarketFromServer(dto.market)
            /**
             * Backend is not sending everything back as it would cause a lot of duplicate data
             * So if the dto doesn't contain any stalls add the dto object for this specific stall to the 
             * type dto. This is to make sure that the types have a complete list of the related stalls.
             */
            if (dto.stallType.stalls == null || dto.stallType.stalls.length === 0) {
                dto.stallType.stalls = [dto]
            }
            this.type = this.store.rootStore.stallTypeStore.updateStallTypeFromServer(dto.stallType);
            this.state = ModelState.IDLE
        }
        return this;
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