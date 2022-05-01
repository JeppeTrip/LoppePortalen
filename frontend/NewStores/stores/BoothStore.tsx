import { action, flow, makeAutoObservable, observable } from "mobx";
import { Booth } from "../@DomainObjects/Booth";
import { RootStore } from "../RootStore";
import { BoothBaseVM as Dto, BoothClient, GetFilteredBoothsResponse} from '../../services/clients';

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
        const res = yield this.transportLayer.getBooth(boothId);
        const booth =  this.updateBoothFromServer(res.booth)
        return booth
    }

    @action
    fetchFilteredBooths(startDate?: Date, endDate?: Date, categories?: string[])
    {
        this.booths = []
        this.transportLayer.filteredBooths(startDate, endDate, categories)
        .then(
            action("fetchSuccess", result => {
                result.booths.forEach(dto => {
                    this.updateBoothFromServer(dto)
                });
            }),
            action("fetchError", error => {
                console.log(error)
                this.booths = []
            })
        )
    }
}