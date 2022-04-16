import { makeAutoObservable } from "mobx"
import { AuthorizationClient, MarketClient, OrganiserClient, StallClient, StallTypeClient, UserClient } from "../services/clients"
import { AuthStore } from "./stores/AuthStore"
import { MarketStore } from "./stores/MarketStore"
import { OrganiserStore } from "./stores/OrganiserStore"
import { StallStore } from "./stores/StallStore"
import { StallTypeStore } from "./stores/StallTypeStore"
import { UserStore } from "./stores/UserStore"

export class RootStore {
    authStore : AuthStore
    userStore : UserStore
    organiserStore : OrganiserStore
    marketStore : MarketStore
    stallTypeStore : StallTypeStore
    stallStore : StallStore

    constructor(){
        makeAutoObservable(this)
        this.authStore = new AuthStore(this, new AuthorizationClient())
        this.userStore = new UserStore(this, new UserClient())
        this.organiserStore = new OrganiserStore(this, new OrganiserClient())
        this.marketStore = new MarketStore(this, new MarketClient())
        this.stallTypeStore = new StallTypeStore(this, new StallTypeClient())
        this.stallStore = new StallStore(this, new StallClient())
    }
}