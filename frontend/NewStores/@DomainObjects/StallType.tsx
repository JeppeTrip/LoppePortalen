import { action, makeAutoObservable, observable, reaction, autorun, computed } from "mobx"
import { ModelState } from "../../@types/ModelState"
import { StallTypeStore } from "../stores/StallTypeStore"
import { Stall } from "./Stall"
import { StallType as Dto } from '../../services/clients'
import { Market } from "./Market"


export class StallType {
    store: StallTypeStore
    @observable state: symbol
    @observable id: number
    @observable name: string
    @observable description: string
    @observable stalls: Stall[]
    @observable market: Market
    @observable totalStallCount: number


    constructor(store) {
        makeAutoObservable(this);
        this.name = ""
        this.description = ""
        this.store = store
        this.stalls = [] as Stall[]
        this.state = ModelState.NEW
        this.market = null
        this.totalStallCount = -1
    }

    @action
    updateFromServer(dto: Dto) {
        if (this.state != ModelState.UPDATING) {
            this.state = ModelState.UPDATING
            this.id = dto.id
            this.name = dto.name
            this.totalStallCount = dto.totalStallCount
            this.description = dto.description
            dto.stalls?.forEach(x => {
                const stall = this.store.rootStore.stallStore.updateStallFromServer(x)
                if (!this.stalls.find(s => s.id === stall.id)) {
                    this.stalls.push(stall)
                }
            })
            if (dto.market != null) {
                this.market = this.store.rootStore.marketStore.updateMarketFromServer(dto.market)
            }
            this.state = ModelState.IDLE
        }
        return this
    }

    @action
    select() {
        this.store.selectedStallType = this;
    }

    @action
    deselect() {
        if (this.store.selectedStallType.id === this.id) {
            this.store.selectedStallType = null;
        }
    }

    @action
    save() {
        this.state = ModelState.SAVING
        this.store.transportLayer.createStallType({
            marketId: this.market.id,
            name: this.name,
            description: this.description
        }).then(
            action("submitSuccess", res => {
                if (res.succeeded) {
                    this.updateFromServer({
                        id: res.id,
                        name: res.name,
                        description: res.description
                    })
                    this.state = ModelState.IDLE
                    if (this.store.newStallType?.id === this.id)
                        this.store.newStallType = null
                } else {
                    this.state = ModelState.ERROR
                }
            }),
            action("submitError", error => {
                this.state = ModelState.ERROR
            })
        )
    }

    /**
     * Adds a number of stalls. 
     * The update to the class list is only made after all stalls have been
     * created this is to esnure the action that updates the related market
     * doesn't run a million times.
     * @param count the number of stalls to add.
     */
    @action
    addStalls(count: number) {
        const stalls = [] as Stall[]
        let stall: Stall
        for (var i = 0; i < count; i++) {
            stall = this.store.rootStore.stallStore.createStall()
            stalls.push(stall)
        }
        this.stalls = this.stalls.concat(stalls)
    }

    @action
    saveNewStallsToMarket(count: number) {
        this.state = ModelState.SAVING
        this.store.rootStore.marketStore.transportLayer.addStallsToMarket(
            {
                marketId: this.market.id,
                stallTypeId: this.id,
                number: count
            }
        ).then(
            action("submitSuccess", res => {
                if (res.succeeded) {
                    res.stalls.forEach(x => this.store.rootStore.stallStore.updateStallFromServer(x))
                    this.state = ModelState.IDLE
                }
                else {
                    this.state = ModelState.ERROR
                }
            }),
            action("submitError", error => {
                this.state = ModelState.ERROR
            })
        )
    }

    @action
    //TODO: find out how to propegate this change through out the application
    removeStalls(count: number) {
        console.log(count)
        this.stalls.splice(0, Math.min(this.stalls.length, count))
    }

    /**
     * Remove stall with the given id from the stall list of this stalltype.
     * Internal change only. Nothing comitted to the database.
     */
    @action
    removeStall(id : number)
    {
        this.stalls = this.stalls.filter(x => x.id != id)
    }

    @action
    set setName(name: string) {
        this.name = name
    }

    @action
    set setDescription(description: string) {
        this.description = description
    }
}