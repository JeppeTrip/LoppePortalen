import { action, makeAutoObservable, observable } from "mobx";
import { Stall } from "../@DomainObjects/Stall";
import { RootStore } from "../RootStore";


export class StallStore{
    rootStore : RootStore
    transportLayer
    @observable stalls : Stall[]
    
    constructor(rootStore : RootStore, transportLayer)
    {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
        this.stalls = [] as Stall[]
    }

    @action
    createStall()
    {
        const stall = new Stall(this)
        this.stalls.push(stall)
        return stall
    }
}