import { action, autorun, computed, makeAutoObservable, observable } from "mobx";
import { Market as Dto, ValueTupleOfStringAndStringAndInteger } from "../../services/clients";
import { MarketStore } from "../stores/MarketStore";
import { Organiser } from "./Organiser";
import { Stall } from "./Stall";
import { StallType } from "./StallType";

export class Market {
    store: MarketStore
    @observable state: string = "idle"
    @observable oldState: Market
    @observable id: number
    @observable organiserId: number
    @observable organiser: Organiser
    @observable name: string
    @observable description: string
    @observable startDate: Date
    @observable endDate: Date
    @observable isCancelled: boolean
    @observable stallTypes: StallType[]
    @observable stalls: Stall[]

    @action
    set setId(id: number) {
        this.id = id;
    }

    @action
    set setOrganiserId(id: number) {
        this.id = id;
    }

    @action
    set setName(name: string) {
        this.name = name
    }

    @action
    set setDescription(description: string) {
        this.description = description
    }

    @action
    set setStartDate(date: Date) {
        this.startDate = date
    }

    @action
    set setEndDate(date: Date) {
        this.startDate = date
    }

    @action
    set setIsCancelled(isCancelled: boolean) {
        this.isCancelled = isCancelled
    }

    constructor(store: MarketStore) {
        makeAutoObservable(this);
        this.store = store
        this.stallTypes = [] as StallType[]
        this.startDate = null
        this.endDate = null
        this.oldState = null
    }

    @action
    updateFromServer(dto: Dto) {
        console.log("market update from server:")
        console.log(dto)
        if (this.state != "updating") {
            this.state = "updating"
            this.id = dto.marketId
            this.name = dto.marketName
            this.description = dto.description
            this.startDate = new Date(dto.startDate)
            this.endDate = new Date(dto.endDate)
            this.isCancelled = dto.isCancelled
            /**
             * Backend doesn't set the market value for the organiser 
             * if we are fetching markets as this would return A LOT of duplicate data.
             * Therefor check if the dto value for markets if so, add in an array with this market dto in it.
             */
            if (dto.organiser.markets === null) {
                dto.organiser.markets = [dto]
            }
            this.organiser = this.store.rootStore.organiserStore.updateOrganiserFromServer(dto.organiser)
            this.setOldState()
            this.state = "idle"
        }
        return this;
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
                stallTypes: this.stallTypes.map(x => {
                    return {
                        item1: x.name,
                        item2: x.description,
                        item3: x.stalls.length
                    } as ValueTupleOfStringAndStringAndInteger
                })
            }).then(
                action("submitSuccess", res => {
                    this.id = res.market.marketId,
                        this.setOldState();
                    this.store.markets.push(this);
                }),
                action("submitError", error => {
                    //do something with the error.
                })
            )
        }
        else {
            this.store.transportLayer.updateMarket({
                marketId: this.id,
                organiserId: this.organiserId,
                marketName: this.name,
                description: this.description,
                startDate: this.startDate,
                endDate: this.endDate
            }).then(
                action("submitSuccess", res => {
                    if (res.succeeded) {
                        this.setOldState();
                    }
                }),
                action("submitError", error => {
                    //do something with the error.
                })
            )
        }
    }

    @action
    addNewStallType() {
        const type = this.store.rootStore.stallTypeStore.createStallType()
        this.stallTypes.push(type);
        return type;
    }

    /**
     * Used internally to store the state of the component.
     * This is used for editing if you want to cancel your changes.
     */
    private setOldState() {
        const state = new Market(null);
        state.name = this.name;
        state.description = this.description;
        state.startDate = new Date(this.startDate);
        state.endDate = new Date(this.endDate);
        state.isCancelled = this.isCancelled;
        this.oldState = state;
    }

    @action
    resetState() {
        if (this.oldState && this.oldState != null) {
            this.name = this.oldState.name;
            this.description = this.oldState.description;
            this.startDate = new Date(this.oldState.startDate);
            this.endDate = new Date(this.oldState.endDate);
            this.isCancelled = this.oldState.isCancelled;
        }
    }

    @computed
    get savedStallTypes() {
        return this.stallTypes.filter(x => x.id > 0)
    }

    @computed
    stallTypeChangeReaction() {
        const typeStalls = this.stallTypes.reduce(function (a, b) {
            return a + b.stalls.length;
        }, 0)
        console.log("stall types")
        console.log(this.stallTypes.length)
        console.log("stalls in those types")
        console.log(typeStalls)
        return typeStalls

    }


}