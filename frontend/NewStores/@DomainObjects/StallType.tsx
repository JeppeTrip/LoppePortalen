import { action, makeAutoObservable, observable, reaction, autorun, computed  } from "mobx"
import { ModelState } from "../../@types/ModelState"
import { StallTypeStore } from "../stores/StallTypeStore"
import { Stall } from "./Stall"
import {StallType as Dto } from '../../services/clients'


export class StallType {
    store : StallTypeStore
    @observable state : symbol
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
        this.state = ModelState.NEW
    }

    @action
    updateFromServer(dto : Dto)
    {
        if(this.state != ModelState.UPDATING)
        {
            this.state = ModelState.UPDATING
            this.id = dto.id
            this.name = dto.name
            this.description = dto.description
            dto.stalls?.forEach(x => {
                const stall = this.store.rootStore.stallStore.updateStallFromServer(x)
                if(!this.stalls.find(s => s.id === stall.id))
                {
                    this.stalls.push(stall)
                }
            })
            this.state = ModelState.IDLE
        }
        return this
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