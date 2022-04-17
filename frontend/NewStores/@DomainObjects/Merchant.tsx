import { action, makeAutoObservable, observable } from "mobx"
import { ModelState } from "../../@types/ModelState"
import { MerchantStore } from "../stores/MerchantStore"
import {Merchant as Dto} from "../../services/clients"


export class Merchant{
    store : MerchantStore
    @observable _id : number
    @observable _state : ModelState
    @observable _name : string
    @observable _description : string
    @observable _userId : string
    
    constructor(store : MerchantStore)
    {
        makeAutoObservable(this)
        this.store = store
        this.state = ModelState.NEW
        this.name = ""
        this.description = ""
    }

    /**
     * Try and push this merchant to the server.
     * Updates state to saving.
     * If successful update state to idle. 
     * If error is encountered update state to error.
     */
    @action
    save()
    {
        this.store.saveMerchant(this)
    }

    @action
    updateFromServer(dto : Dto)
    {
        if(this.state != ModelState.UPDATING)
        {
            this.state = ModelState.UPDATING
            this._id = dto.id
            this.name = dto.name
            this.description = dto.description
            this._userId = dto.userId
            this.state = ModelState.IDLE
        }
        return this
    }

    get state()
    {
        return this._state
    }

    set state(state : ModelState)
    {
        this._state = state
    }

    get name()
    {
        return this._name
    }

    set name(name : string)
    {
        this._name = name
    }

    get description()
    {
        return this._description
    }

    set description(description : string)
    {
        this._description = description
    }

    get id()
    {
        return this._id
    }

    get userId()
    {
        return this._userId
    }

}