import { makeAutoObservable } from "mobx"
import { AuthorizationClient } from "../stores/models"
import { AuthStore } from "./stores/AuthStore"
import { UserStore } from "./stores/UserStore"

export interface IRootStore {
    authStore : AuthStore
    userStore : UserStore
}

export class RootStore {
    authStore : AuthStore
    userStore : UserStore

    constructor(){
        makeAutoObservable(this)
        this.authStore = new AuthStore(this, new AuthorizationClient())
        this.userStore = new UserStore(this, null)
    }
}