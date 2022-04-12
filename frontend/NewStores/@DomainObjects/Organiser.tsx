import { makeAutoObservable, observable } from "mobx"
import { OrganiserStore } from "../stores/OrganiserStore"

export class Organiser{
    store : OrganiserStore
    id : number = null
    userId : string = null
    @observable name : string = ""
    @observable description : string = ""
    @observable street : string = ""
    @observable streetNumber : string = ""
    @observable appartment : string = ""
    @observable postalCode : string = ""
    @observable city : string = ""

    constructor(store : OrganiserStore, id? : number, userId? : string)
    {
        makeAutoObservable(this)
        this.store = store
        this.id = id
        this.userId = userId
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