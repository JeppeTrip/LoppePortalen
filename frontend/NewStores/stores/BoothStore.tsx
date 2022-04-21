import { action, flow, makeAutoObservable, observable } from "mobx";
import { Booth } from "../@DomainObjects/Booth";
import { RootStore } from "../RootStore";
import { Booth as Dto, BoothClient} from '../../services/clients';

export class BoothStore {
    rootStore: RootStore
    transportLayer : BoothClient
    @observable booths: Booth[]

    constructor(rootStore: RootStore, transportLayer : BoothClient) {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
        this.booths = [] as Booth[]
    }

    @action
    updateBoothFromServer(dto : Dto) {
        let booth = this.booths.find(x => x.id === dto.id)
        if (!booth) {
            booth = new Booth(this);
            this.booths.push(booth)
        }
        booth.updateFromServer(dto)
        return booth;
    }

    @flow 
    *fetchBooth(boothId : string)
    {
        const res : Dto = yield this.transportLayer.getBooth(boothId);
        const booth =  this.updateBoothFromServer(res)
        return booth
    }
}