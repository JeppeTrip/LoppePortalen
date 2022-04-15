import { makeAutoObservable, observable } from "mobx";
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

    createStallType()
    {
        const type = new StallType(this)
        this.stallTypes.push(type)
        return type
    }
}