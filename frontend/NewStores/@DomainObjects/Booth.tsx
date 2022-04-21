import { action, makeAutoObservable, observable } from "mobx"
import { BoothStore } from "../stores/BoothStore"
import {Booth as Dto} from "../../services/clients"
import { Stall } from "./Stall"
import { ModelState } from "../../@types/ModelState"

export class Booth{
    store : BoothStore
    @observable state : symbol
    @observable id : string
    @observable name : string
    @observable description : string
    @observable stall : Stall

    constructor(store : BoothStore)
    {
        makeAutoObservable(this)
        this.store = store
        this.state = ModelState.NEW
    }

    @action
    updateFromServer(dto : Dto)
    {
        if(this.state != ModelState.UPDATING)
        {
            this.id = dto.id
            this.name = dto.boothName 
            this.description = dto.boothDescription
            this.stall = this.store.rootStore.stallStore.updateStallFromServer(dto.stall)

            this.state = ModelState.IDLE
        }
        return this;
        
    }
}