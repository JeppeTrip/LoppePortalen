import { action, makeAutoObservable, observable } from "mobx";
import { Market as Dto } from "../../stores/models";
import { MarketStore } from "../stores/MarketStore";

export class Market {
    store: MarketStore
    @observable id: number
    @observable organiserId: number
    @observable name: string
    @observable description: string
    @observable startDate: Date
    @observable endDate: Date
    @observable isCancelled: boolean

    constructor(store: MarketStore) {
        makeAutoObservable(this);
        this.store = store
    }

    @action
    update(dto: Dto) {
        this.id = dto.marketId
        this.organiserId = dto.organiserId
        this.name = dto.marketName
        this.description = dto.description
        this.startDate = new Date(dto.startDate)
        this.endDate = new Date(dto.endDate)
        this.isCancelled = dto.isCancelled
    }

    @action
    select() {
        this.store.selectedMarket = this;
    }

    @action
    save() {
        if (!this.id) {
            this.store.transportLayer.createMarket({
                organiserId: this.organiserId,
                marketName: this.name,
                description: this.description,
                startDate: this.startDate,
                endDate: this.endDate,
                stalls: [] //TODO :Update
            }).then(
                action("submitSuccess", res => {
                    this.id = res.marketId,
                        this.store.markets.push(this);
                }),
                action("submitError", error => {
                    //do something with the error.
                })
            )
        }
    }
}