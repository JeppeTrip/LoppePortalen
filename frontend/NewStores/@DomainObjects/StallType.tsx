import { action, makeAutoObservable, observable, reaction, autorun  } from "mobx"
import { StallTypeStore } from "../stores/StallTypeStore"
import { Stall } from "./Stall"


export class StallType {
    store : StallTypeStore
    @observable id : number
    @observable name : string
    @observable description : string
    @observable stalls : Stall[]


    constructor(store)
    {
        makeAutoObservable(this);
        this.name = ""
        this.description = ""
        this.store = store
        this.stalls = [] as Stall[]
    }

    @action
    updateFromServer(dto)
    {
        //TODO: Implement
    }

    @action
    select()
    {
        this.store.selectedStallType = this;
    }

    @action
    deselect(){
        if(this.store.selectedStallType.id === this.id)
        {
            this.store.selectedStallType = null;
        }
    }

    @action
    save()
    {
        this.id = 1
    }

    /**
     * Adds a number of stalls. 
     * The update to the class list is only made after all stalls have been
     * created this is to esnure the action that updates the related market
     * doesn't run a million times.
     * @param count the number of stalls to add.
     */
    @action
    addStalls(count : number)
    {
        const stalls = [] as Stall[]
        let stall : Stall
        for(var i=0; i<count; i++)
        {
            stall = this.store.rootStore.stallStore.createStall()
            stalls.push(stall)
        }
        this.stalls = this.stalls.concat(stalls)
    }
    
    @action
    set setName(name : string) {
        this.name = name
    }

    @action
    set setDescription(description : string) {
        this.description = description
    }
}