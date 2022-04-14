import { action, makeAutoObservable, observable } from "mobx"
import { OrganiserStore } from "../stores/OrganiserStore"
import { Organiser as Dto } from "../../stores/models";
import { Market } from "./Market";

export class Organiser {
    store: OrganiserStore
    state: string = "idle"
    @observable id: number = null
    @observable userId: string = null
    @observable name: string = ""
    @observable description: string = ""
    @observable street: string = ""
    @observable streetNumber: string = ""
    @observable appartment: string = ""
    @observable postalCode: string = ""
    @observable city: string = ""
    @observable markets: Market[]

    constructor(store: OrganiserStore, id?: number) {
        makeAutoObservable(this)
        this.store = store
        this.id = id
        this.userId = ""
        this.markets = [] as Market[]
    }

    /**
     * Updates the entity itself based on the generated model in the backend.
     */
    @action
    updateFromServer(dto: Dto) {
        if (this.state != "updating") {
            this.state = "updating"
            this.id = dto.id
            this.userId = dto.userId
            this.name = dto.name
            this.description = dto.description
            this.street = dto.street
            this.streetNumber = dto.streetNumber
            this.appartment = dto.appartment
            this.postalCode = dto.postalCode
            this.city = dto.city
            dto.markets.forEach(mDto => {
                let res = this.store.rootStore.marketStore.updateMarketFromServer(mDto)
                let market = this.markets.length === 0 ? undefined :  this.markets.find(x => x.id === res.id);
                if(!market)
                {
                    this.markets.push(res)
                }
            })
            this.state = "idle"
        }
        return this;
    }

    @action
    select() {
        console.log("Select organiser")
        console.log(this)
        this.store.selectedOrganiser = this;
    }

    /**
     * submit things to the server.
     */
    @action
    save() {
        if (!this.id) {
            this.store.transportLayer.createOrganiser({
                userId: this.userId,
                name: this.name,
                description: this.description,
                street: this.street,
                number: this.streetNumber,
                appartment: this.appartment,
                city: this.city,
                postalCode: this.postalCode
            }).then(
                action("submitSuccess", res => {
                    this.id = res.id,
                        this.store.organisers.push(this);
                }),
                action("submitError", error => {
                    //do something with the error.
                })
            )
        }
    }

    set setName(name: string) {
        this.name = name
    }

    set setDescription(description: string) {
        this.description = description
    }

    set setStreet(street: string) {
        this.street = street
    }

    set setStreetNumber(streetNumber: string) {
        this.streetNumber = streetNumber
    }

    set setAppartment(appartment: string) {
        this.appartment = appartment
    }

    set setPostalCode(postalCode: string) {
        this.postalCode = postalCode
    }

    set setCity(city: string) {
        this.city = city
    }
}