import { action, makeAutoObservable, observable } from "mobx"
import { OrganiserStore } from "../stores/OrganiserStore"
import { CreateOrganiserRequest, EditOrganiserRequest, GetOrganiserVM, OrganiserBaseVM as Dto } from "../../services/clients";
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
     * Push market instance into the markets list if it doesn't exist there already.
     */
    @action
    addMarket(market : Market)
    {
        const res = this.markets.find(x => x.id === market.id)
        if(!res)
            this.markets.push(market)
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

            switch(dto.constructor.name){
                case "GetOrganiserVM": this.updateFromServerGetOrganiserVm(dto);
            }

            this.state = ModelState.IDLE
        }
        return this;
    }

    @action
    private updateFromServerGetOrganiserVm(dto : GetOrganiserVM)
    {
        let currentMarket : Market;
        dto.markets.forEach(x => {
            currentMarket = this.store.rootStore.marketStore.updateMarketFromServer(x)
            currentMarket.organiser = this
            this.markets.push(currentMarket)
        })
    }

    @action
    select() {
        this.store.selectedOrganiser = this;
    }

    /**
     * submit things to the server.
     */
    @action
    save() {
        this.state = ModelState.SAVING
        if (!this.id) {
            this.store.transportLayer.createOrganiser(new CreateOrganiserRequest({
                name: this.name,
                description: this.description,
                street: this.street,
                number: this.streetNumber,
                appartment: this.appartment,
                city: this.city,
                postalCode: this.postalCode
            })).then(
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
            this.store.transportLayer.editOrganiser(new EditOrganiserRequest({
                organiserId: this.id,
                name: this.name,
                description: this.description,
                street: this.street,
                number: this.streetNumber,
                appartment: this.appartment,
                city: this.city,
                postalCode: this.postalCode
            }))
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