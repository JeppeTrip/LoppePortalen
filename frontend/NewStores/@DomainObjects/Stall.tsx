import { action, makeAutoObservable, observable } from "mobx";
import { StallStore } from "../stores/StallStore";
import { StallType } from "./StallType";


export class Stall{
    store : StallStore
    @observable id : number
    @observable type : StallType

    constructor(store : StallStore)
    {
        makeAutoObservable(this)
        this.store = store
    }

    @action
    set setId(id : number)
    {
        this.id = id
    }

    @action
    set setType(stallType : StallType)
    {
        this.type = stallType
    }
}