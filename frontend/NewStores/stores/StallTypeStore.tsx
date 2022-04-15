import { action, makeAutoObservable, observable } from "mobx";
import { Stall } from "../../@types/Stall";
import { StallType } from "../@DomainObjects/StallType";
import { RootStore } from "../RootStore";


export class StallTypeStore {
    rootStore : RootStore
    transportLayer
    @observable stallTypes : StallType[]
    @observable selectedStallType : StallType

    constructor(rootStore : RootStore, transportLayer?)
    {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
        this.stallTypes = [] as StallType[]
    }

    @action
    createStallType()
    {
        const type = new StallType(this)
        this.stallTypes.push(type)
        return type
    }

    @action
    updateStallTypeFromServer(dto)
    {
        let type = this.stallTypes.find(x => x.id === dto.id)
        if(!type)
        {
            type = this.createStallType()
        }
        type.updateFromServer(dto)
        return type
    }
}