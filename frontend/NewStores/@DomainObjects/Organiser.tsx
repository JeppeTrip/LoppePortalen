import { action, makeAutoObservable, observable } from "mobx"
import { OrganiserStore } from "../stores/OrganiserStore"
import {Organiser as Dto } from "../../stores/models";
import { Market } from "./Market";

export class Organiser{
    store : OrganiserStore
    @observable id : number = null
    @observable userId : string = null
    @observable name : string = ""
    @observable description : string = ""
    @observable street : string = ""
    @observable streetNumber : string = ""
    @observable appartment : string = ""
    @observable postalCode : string = ""
    @observable city : string = ""
    @observable markets : Market[]

    constructor(store : OrganiserStore, id? : number, userId? : string)
    {
        makeAutoObservable(this)
        this.store = store
        this.id = id
        this.userId = userId
        this.markets = [] as Market[]
    }

    /**
     * Updates the entity itself based on the generated model in the backend. 
     */
    @action
    update(dto : Dto){
        this.id = dto.id
        this.userId = dto.userId
        this.name = dto.name
        this.description = dto.description
        this.street = dto.street
        this.streetNumber = dto.streetNumber
        this.appartment = dto.appartment
        this.postalCode = dto.postalCode
        this.city = dto.city
        this.markets = this.store.rootStore.marketStore.updateMarketstFromServer(dto.markets)
    }

    @action
    select(){
        this.store.selectedOrganiser = this;
    }

    set setName(name : string)
    {
        this.name = name
    }

    set setDescription(description : string)
    {
        this.description = description
    }

    set setStreet(street : string)
    {
        this.street = street
    }

    set setStreetNumber(streetNumber : string)
    {
        this.streetNumber = streetNumber
    }

    set setAppartment(appartment : string)
    {
        this.appartment = appartment
    }

    set setPostalCode(postalCode : string)
    {
        this.postalCode = postalCode
    }

    set setCity(city : string)
    {
        this.city = city
    }
}