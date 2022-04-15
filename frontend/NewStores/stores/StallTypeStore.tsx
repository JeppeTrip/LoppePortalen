import { action, makeAutoObservable, observable } from "mobx";
import { Stall } from "../../@types/Stall";
import { StallTypeClient } from "../../services/clients";
import { Market } from "../@DomainObjects/Market";
import { StallType } from "../@DomainObjects/StallType";
import { RootStore } from "../RootStore";


export class StallTypeStore {
    rootStore : RootStore
    transportLayer : StallTypeClient
    @observable stallTypes : StallType[]
    @observable selectedStallType : StallType
    @observable newStallType : StallType


    constructor(rootStore : RootStore, transportLayer : StallTypeClient)
    {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
        this.stallTypes = [] as StallType[]
        this.transportLayer = transportLayer
        this.newStallType = null
        this.selectedStallType = null
    }

    @action
    createStallType()
    {
        const type = new StallType(this)
        this.stallTypes.push(type)
        this.newStallType = type
        return type
    }
    
    @action
    updateStallTypeFromServer(dto)
    {
        let type = this.stallTypes.find(x => x.id === dto.id)
        if(!type)
        {
            type = new StallType(this)
            this.stallTypes.push(type)
        }
        type.updateFromServer(dto)
        return type
    }
}