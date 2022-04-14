import { action, flow, makeAutoObservable } from "mobx";
import { UserClient } from "../../services/clients";
import { User } from "../@DomainObjects/User";
import { RootStore } from "../RootStore";

export class UserStore {
    rootStore: RootStore
    user: User = null
    transportLayer: UserClient

    state: string = "pending"

    constructor(rootStore, transportLayer) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        this.transportLayer = transportLayer;
    }

    /**
     * Create a new user object.
     * Doesn't submit anything to the backend.
     */
    @action
    createUser() {
        const user = new User(this);
        this.user = user;
        return user;
    }

    /**
    * Tries to resolve the user information for the user
    * currently signed in to the website.
    * Tries to fetch the user if it succeeds set the user object otherwise set it to null.
    * Return this.user under any circumstance so the auth object can update it state based on the 
    * result of promise.
    */
    @flow
    *resolveCurrentUser() {
        try {
            const result = yield this.transportLayer.getUserInfo()
            console.log(result);
            if (result.succeeded) {
                const user = new User(this, result.id)
                this.user = user;
                user.updateFromServer(result.user)
            } else {
                this.user = null
            }
        }
        catch (error) {
            this.user = null
        }
        finally {
            return this.user;
        }
    }

    @action
    fetchOwnedMarkets()
    {

    }

}