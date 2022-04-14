import { action, computed, makeAutoObservable, observable } from "mobx";
import { Market as Dto } from "../../stores/models";
import { MarketStore } from "../stores/MarketStore";
import { Organiser } from "./Organiser";
import { Stall } from "./Stall";

export class Market {
    store: MarketStore
    state : string = "idle"
    oldState : Market 
    @observable id: number
    @observable organiserId: number
    @observable organiser : Organiser
    @observable name: string
    @observable description: string
    @observable startDate: Date
    @observable endDate: Date
    @observable isCancelled: boolean
    @observable stalls : Stall[]
    @observable selectedStall : Stall

    @action
    set setId(id : number)
    {
        this.id = id;
    }

    @action
    set setOrganiserId(id : number)
    {
        this.id = id;
    }

    @action
    set setName(name : string){
        this.name = name
    }

    @action
    set setDescription(description : string)
    {
        this.description = description
    }

    @action
    set setStartDate(date : Date)
    {
        this.startDate = date
    }

    @action
    set setEndDate(date : Date)
    {
        this.startDate = date
    }

    @action
    set setIsCancelled(isCancelled : boolean)
    {
        this.isCancelled = isCancelled
    }

    @action
    set setSelectedStall(stall : Stall)
    {
        this.selectedStall = stall
    }
    
    constructor(store: MarketStore) {
        makeAutoObservable(this);
        this.store = store
        this.stalls = [] as Stall[]
        this.selectedStall = null
        this.startDate = null
        this.endDate = null
        this.oldState = null
    }

    @action
    updateFromServer(dto: Dto) {
        console.log("market update from server:")
        console.log(dto)
        if(this.state != "updating")
        {
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
            if(dto.organiser.markets === null)
            {
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
                stalls: this.uniqueStalls.map(x => {return {name: x.name, description: x.description, count: this.stallCount(x.name)}})//TODO :Update
            }).then(
                action("submitSuccess", res => {
                    this.id = res.marketId,
                    this.setOldState();
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

    @computed
    stallCount(type : string)
    {
        return this.stalls.filter(x => x.name === type).length;
    }

    @action
    createStall(){
        const stall = new Stall();
        this.selectedStall = stall;
        return stall;
    }

        //TODO: This is temporary (likely)
        @action
        setNumberOfStalls(type : string, count : number)
        {
            var unique = this.stalls.filter(x => x.name === type);
            var stall = unique[0];
    
            if(stall === null ||stall === undefined)
            {
                return;
            }
            count = count < 1 ? 1 : count;
            const currentCount = unique.length;
            const diff = count - currentCount;
    
            if(diff > 0)
            {
                var newStalls : Stall[] = [];
                for(var i = 0; i<diff; i++)
                {
                    newStalls.push(stall)
                }
                this.stalls = this.stalls.concat(newStalls);
            }
            else if(diff < 0)
            {
                this.stalls = this.stalls.slice(0, diff);
            }
        }

        /**
         * Used internally to store the state of the component.
         * This is used for editing if you want to cancel your changes.
         */
    private setOldState()
    {
        const state = new Market(null);
        state.name = this.name;
        state.description = this.description;
        state.startDate = new Date(this.startDate);
        state.endDate = new Date(this.endDate);
        state.isCancelled = this.isCancelled;
        this.oldState = state;
    } 

    @action
    resetState()
    {
        if(this.oldState && this.oldState != null)
        {
            this.name = this.oldState.name;
            this.description = this.oldState.description;
            this.startDate = new Date(this.oldState.startDate);
            this.endDate = new Date(this.oldState.endDate);
            this.isCancelled = this.oldState.isCancelled; 
        }
    }
}