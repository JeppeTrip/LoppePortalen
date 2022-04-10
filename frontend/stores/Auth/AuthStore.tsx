import { action, makeAutoObservable, observable } from "mobx";
import { AuthenticateUserRequest, AuthorizationClient } from "../models";
import { RootStore } from "../RootStore";

const tokenPath = "loppeportalen_user"

class AuthStore {
    rootStore: RootStore;

    @observable signedIn: boolean;
    @observable initializing: boolean;
    @observable redirect: string;

    //set default values and run initialise after.
    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        this.signedIn = false;
        this.initializing = false;
        this.redirect = null;
    }

    @action
    public initialize() {
        this.initializing = true;
        var key = localStorage.getItem(tokenPath);
        if(key != null)
        {
            this.signedIn = true;
        } else {
            this.signedIn = false;
        }
        this.initializing = false;
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
                    localStorage.setItem(tokenPath, res.token);
                }
                this.signedIn = res.succeeded
            }).catch(error => {
                this.signedIn = false;
            });
    }

    @action
    logout() {
        this.signedIn = false;
        localStorage.removeItem(tokenPath);

    }

    @action
    setRedirect(path: string) {
        console.log("AuthStore set redirect path: "+path)
        this.redirect = path;
    }

    @action
    consumeRedirect() {
        console.log("consumeRedirect value: "+this.redirect);
        if(this.redirect == null)
        {
            return this.redirect;
        }
        const res = this.redirect.valueOf();
        console.log("consumeRedirect.valueOf: "+res);
        this.redirect = null;;
        return res;
    }

    @action
    setSignedIn(isSignedIn : boolean){
        this.signedIn = isSignedIn;
    }
}

export { AuthStore }