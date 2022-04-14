import { action, flowResult, makeAutoObservable, observable } from "mobx";
import { UserStore } from "../stores/UserStore";
import { Market } from "./Market";
import { Organiser } from "./Organiser";
import {User as Dto} from "../../stores/models";

export class User {
    store: UserStore = null
    id: string = null
    @observable firstName: string = ""
    @observable lastName: string = ""
    @observable email: string = "" //get this from the auth object?
    @observable phoneNumber: string = ""
    @observable dateOfBirth: Date = null
    @observable country: string = ""
    @observable organisers: Organiser[]
    @observable markets: Market[]

    //organisers form a organiser store

    constructor(store: UserStore, id?: string) {
        makeAutoObservable(this)
        this.store = store
        this.id = id
        this.organisers = [] as Organiser[]
        this.markets = [] as Market[]
    }

    @action
    save() {
        console.log("NOT IMPLEMENTED YET WHAT?")
    }

    @action
    resetChanges() {
        console.log("NOT IMPLEMENTED YET")
    }

    //TODO: load user information automatically.
    @action
    fetchOwnedOrganisers() {
        flowResult(this.store.rootStore.organiserStore.resolveOrganisersFiltered(this.id))
            .then(
                action("resolvedUsersOrganisers", orgs => {
                    console.log("back in user")
                    console.log(orgs)
                    this.organisers = orgs;
                }),
                action("resolvedUsersOrganisersFailed", orgs => {
                    this.organisers = null
                })
            ).catch(error => this.organisers = null)
        return null
    }

    @action
    fetchOwnedMarkets(){
        this.markets = []
        this.store.transportLayer.getUsersMarkets()
        .then(
            action("fetchSuccess", result => {
                result.markets.forEach(x => {
                    let market = this.store.rootStore.marketStore.updateMarketFromServer(x)
                    this.markets.push(market)
                })
            }),
            action("fetchFailed", result => {
                //do something with this.
            })
        )
        .catch(error => this.organisers = null)
    }

    @action
    updateFromServer(dto : Dto){
        this.firstName = dto.firstName
        this.lastName = dto.lastName
        this.dateOfBirth = new Date(dto.dateOfBirth)
        this.email = dto.email //duplicate information probably bad.
        this.phoneNumber = dto.phoneNumber
        this.country = dto.country
        return this;
    }

    set setFirstName(name: string) {
        this.firstName = name
    }

    set setLastName(name: string) {
        this.lastName = name
    }

    set setEamil(email: string) {
        this.email = email;
    }

    set setPhoneNumber(phoneNumber: string) {
        this.phoneNumber = phoneNumber;
    }

    set setDateOfBirth(date: Date) {
        this.dateOfBirth = date;
    }

    set setCountry(country: string) {
        this.country = country
    }
}