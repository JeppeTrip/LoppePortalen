import { action, makeAutoObservable, observable } from "mobx"
import { OrganiserStore } from "../stores/OrganiserStore"
import { OrganiserBaseVM as Dto } from "../../services/clients";
import { Market } from "./Market";
import { ModelState } from "../../@types/ModelState";

export class Organiser {
    store: OrganiserStore
    @observable state: symbol
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
        this.state = ModelState.NEW
    }

    /**
     * Updates the entity itself based on the generated model in the backend.
     */
    @action
    updateFromServer(dto: Dto) {
        if (this.state != ModelState.UPDATING) {
            this.state = ModelState.UPDATING
            this.id = dto.id
            this.userId = dto.userId
            this.name = dto.name
            this.description = dto.description
            this.street = dto.street
            this.streetNumber = dto.streetNumber
            this.appartment = dto.appartment
            this.postalCode = dto.postalCode
            this.city = dto.city
            this.state = ModelState.IDLE
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
        this.state = ModelState.SAVING
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
                    this.state = ModelState.IDLE
                }),
                action("submitError", error => {
                    this.state = ModelState.ERROR
                })
            )
        }
        else {
            this.store.transportLayer.editOrganiser({
                organiserId: this.id,
                userId: this.userId,
                name: this.name,
                description: this.description,
                street: this.street,
                number: this.streetNumber,
                appartment: this.appartment,
                city: this.city,
                postalCode: this.postalCode
            })
                .then(
                    action("submitSuccess", res => {
                        this.state = ModelState.IDLE
                    }),
                    action("submitError", error => {
                        this.state = ModelState.ERROR
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