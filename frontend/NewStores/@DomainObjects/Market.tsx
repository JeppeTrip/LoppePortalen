import { action, computed, makeAutoObservable, observable } from "mobx";
import { ModelState } from "../../@types/ModelState";
import { Market as Dto } from "../../services/clients";
import { MarketStore } from "../stores/MarketStore";
import { Organiser } from "./Organiser";
import { Stall } from "./Stall";
import { StallType } from "./StallType";

export class Market {
    store: MarketStore
    @observable state: symbol
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
        this.state = ModelState.NEW
        this.store = store
        this.stallTypes = [] as StallType[]
        this.startDate = null
        this.endDate = null
        this.oldState = null
        this.organiser = null
        this.stalls = [] as Stall[]
    }

    @action
    updateFromServer(dto: Dto) {
        console.log("market update from server:")
        console.log(dto)
        if (this.state != ModelState.UPDATING) {
            this.state = ModelState.UPDATING
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
            /**
             * Update the stall types.
             */
             dto.stallTypes?.forEach(
                 x => {
                    x.market = dto
                    const type = this.store.rootStore.stallTypeStore.updateStallTypeFromServer(x)
                    if(!this.stallTypes.find(t => t.id === type.id))
                    {
                        this.stallTypes.push(type)
                    }
                 })
            
            /**
             * Update the stalls.
             */
            dto.stalls?.forEach(
                x => {
                    if(x.market == null)
                    {
                        x.market = dto
                    }
                    const stall = this.store.rootStore.stallStore.updateStallFromServer(x)
                    if(!this.stalls.find(s => s.id === stall.id))
                    {
                        this.stalls.push(stall)
                    }
                 })
            this.setOldState()
            this.state = ModelState.IDLE
        }
        return this;
    }

    @action
    select() {
        this.store.selectedMarket = this;
    }

    @action
    deselect() {
        if(this.store.selectedMarket?.id === this.id)
        {
            this.store.selectedMarket = null;
        }
    }

    @action
    save() {
        this.state = ModelState.SAVING
        if (!this.id) {
            this.store.transportLayer.createMarket({
                organiserId: this.organiser?.id,
                marketName: this.name,
                description: this.description,
                startDate: this.startDate,
                endDate: this.endDate
            }).then(
                action("submitSuccess", res => {
                    this.id = res.market.marketId,
                    this.setOldState();
                    this.state = ModelState.IDLE
                }),
                action("submitError", error => {
                    this.state = ModelState.ERROR
                })
            )
        }
        else {
            this.store.transportLayer.updateMarket({
                marketId: this.id,
                organiserId: this.organiser?.id,
                marketName: this.name,
                description: this.description,
                startDate: this.startDate,
                endDate: this.endDate,
            }).then(
                action("submitSuccess", res => {
                    if (res.succeeded) {
                        this.setOldState();
                        this.state = ModelState.IDLE
                    }
                    else{
                        this.state = ModelState.ERROR
                    }
                }),
                action("submitError", error => {
                    this.state = ModelState.ERROR
                })
            )
        }
    }

    @action
    addNewStallType() {
        const type = this.store.rootStore.stallTypeStore.createStallType()
        type.market = this
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

    /**
     * Remove stall with the given id from the stall list of this market.
     * Internal change only. Nothing comitted to the database.
     */
     @action
     removeStall(id : number)
     {
         this.stalls = this.stalls.filter(x => x.id != id)
     }
}