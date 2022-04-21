import { action, makeAutoObservable, observable } from "mobx";
import { Booth } from "../@DomainObjects/Booth";
import { RootStore } from "../RootStore";
import { Booth as Dto} from '../../services/clients';

export class BoothStore {
    rootStore: RootStore
    transportLayer
    @observable booths: Booth[]

    constructor(rootStore: RootStore, transportLayer) {
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
}