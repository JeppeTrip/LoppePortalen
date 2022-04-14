import { action, makeAutoObservable, observable } from "mobx"
import { StallTypeStore } from "../stores/StallTypeStore"


export class StallType {
    store : StallTypeStore
    @observable id : number
    @observable name : string
    @observable description : string


    constructor(store)
    {
        makeAutoObservable(this);
        this.name = ""
        this.description = ""
        this.store = store
    }

    @action
    updateFromServer(dto)
    {
        //TODO: Implement
    }

    @action
    select()
    {
        this.store.selected = this;
    }

    @action
    deselect(){
        if(this.store.selected.id === this.id)
        {
            this.store.selected = null;
        }
    }

    @action
    save()
    {
        this.id = 1
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