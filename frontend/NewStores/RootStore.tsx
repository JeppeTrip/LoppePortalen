import { makeAutoObservable } from "mobx"
import { AuthorizationClient, OrganiserClient, UserClient } from "../stores/models"
import { AuthStore } from "./stores/AuthStore"
import { OrganiserStore } from "./stores/OrganiserStore"
import { UserStore } from "./stores/UserStore"

export class RootStore {
    authStore : AuthStore
    userStore : UserStore
    organiserStore : OrganiserStore

    constructor(){
        makeAutoObservable(this)
        this.authStore = new AuthStore(this, new AuthorizationClient())
        this.userStore = new UserStore(this, new UserClient())
        this.organiserStore = new OrganiserStore(this, new OrganiserClient())
    }
}