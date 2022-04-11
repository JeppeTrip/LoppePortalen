import { makeAutoObservable } from "mobx";
import { User } from "../@DomainObjects/User";
import { RootStore } from "../RootStore";

export class UserStore {
    rootStore : RootStore
    user : User
    transportLayer

    constructor(rootStore, transportLayer)
    {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        this.transportLayer = transportLayer;
    }

    createUser(){
        const user = new User(this);
        this.user = user;
        return user;
    }

}