import { action, makeAutoObservable, observable } from "mobx";
import { AuthenticateUserRequest, AuthorizationClient } from "../models";
import { RootStore } from "../RootStore";

class AuthStore {
    rootStore: RootStore;

    @observable signedIn : boolean;
    @observable initializing : boolean;
    @observable redirect : string;


    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        this.signedIn = false;
        this.initializing = false;
        this.redirect = null;
    }

    @action
    login(email: string, password: string) {
        const client = new AuthorizationClient();
        const request = {
            email: email,
            password: password
        } as AuthenticateUserRequest;
        client.authenticateUser(request)
            .then(res => {
                if (res.succeeded) {
                    localStorage.setItem("user", res.token);
                }
                this.signedIn = res.succeeded
            }).catch(error => {
                this.signedIn = false;
            });
    }

    @action
    setRedirect(path : string)
    {
        this.redirect = path;
    }

    @action
    consumeRedirect()
    {
        const res = this.redirect;
        this.setRedirect(null);
        return res;
    }
}

export { AuthStore }