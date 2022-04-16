import { action, makeAutoObservable, observable } from "mobx";
import { Stall } from "../@DomainObjects/Stall";
import { RootStore } from "../RootStore";
import { Stall as Dto, StallClient } from '../../services/clients'


export class StallStore {
    rootStore: RootStore
    transportLayer: StallClient
    @observable stalls: Stall[]

    constructor(rootStore: RootStore, transportLayer: StallClient) {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
        this.stalls = [] as Stall[]
        this.transportLayer = transportLayer
    }

    @action
    updateStallFromServer(dto: Dto) {
        let stall = this.stalls.find(x => x.id === dto.id)
        if (!stall) {
            stall = new Stall(this);
            this.stalls.push(stall)
        }
        stall.updateFromServer(dto)
        return stall;
    }

    @action
    createStall() {
        const stall = new Stall(this)
        this.stalls.push(stall)
        return stall
    }

    @action
    removeStall(id: number) {
        let stall = this.stalls.find(x => x.id === id)
        if (stall) {
            stall.type.removeStall(stall.id)
            stall.market.removeStall(stall.id)
            this.stalls = this.stalls.filter(x => x.id != stall.id)
        }
    }


}