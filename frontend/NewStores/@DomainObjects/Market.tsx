import { action, computed, makeAutoObservable, observable } from "mobx";
import { Market as Dto } from "../../stores/models";
import { MarketStore } from "../stores/MarketStore";
import { Stall } from "./Stall";

export class Market {
    store: MarketStore
    @observable id: number
    @observable organiserId: number
    @observable name: string
    @observable description: string
    @observable startDate: Date
    @observable endDate: Date
    @observable isCancelled: boolean
    @observable stalls : Stall[]
    @observable selectedStall : Stall

    constructor(store: MarketStore) {
        makeAutoObservable(this);
        this.store = store
        this.stalls = [] as Stall[]
        this.selectedStall = null
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

    @computed
    get stallCounts()
    {
        var unique = this.uniqueStalls;
        var stallCounts = unique.map(stall => [stall, this.stalls.filter(x => x.name === stall.name).length])
        return stallCounts;
    }

    @computed
    get uniqueStalls(){
        var result : Stall[] = []
        var stallTypeSet = new Set<string>(this.stalls.map(x => x.name));
        stallTypeSet.forEach(type => {
            result.push(this.stalls.find(x => x.name == type))
        });
        return result;
    }

    @action
    createStall(){
        const stall = new Stall();
        this.selectedStall = stall;
        return stall;
    }
}