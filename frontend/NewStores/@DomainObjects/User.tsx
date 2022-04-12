import { action, flowResult, makeAutoObservable, observable } from "mobx";
import { UserStore } from "../stores/UserStore";
import { Organiser } from "./Organiser";

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

    //organisers form a organiser store

    constructor(store: UserStore, id?: string) {
        makeAutoObservable(this)
        this.store = store
        this.id = id
        this.organisers = [] as Organiser[]
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
    getOrganisers() {
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