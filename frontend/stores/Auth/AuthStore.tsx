import { action, makeAutoObservable, observable } from "mobx";
import { AuthenticateUserRequest, AuthorizationClient } from "../models";
import { RootStore } from "../RootStore";

const jwtPath = "loppeportalen_jwt"
const refreshPath = "loppeportalen_refresh"

class AuthStore {
    rootStore: RootStore;

    @observable signedIn: boolean;
    @observable initializing: boolean;
    @observable redirect: string;

    @observable authenticating: boolean;

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
        this.authenticating = true;
        var key = localStorage.getItem(jwtPath);
        if (key != null) {
            var authClient = new AuthorizationClient();
            authClient.refreshToken({
                token: localStorage.getItem(jwtPath),
                refreshToken: localStorage.getItem(refreshPath)
            }).then(res => {
                if (res.succeeded) {
                    localStorage.setItem(jwtPath, res.token);
                    localStorage.setItem(refreshPath, res.refreshToken);
                    this.signedIn = true;
                } else {
                    this.logout()
                }
            }).catch(error => {
                this.signedIn = false;
            });
        } else {
            this.signedIn = false;
        }
        this.authenticating = false;
        this.initializing = false;
    }

    @action
    login(email: string, password: string) {
        this.authenticating = true;
        const client = new AuthorizationClient();
        const request = {
            email: email,
            password: password
        } as AuthenticateUserRequest;
        client.authenticateUser(request)
            .then(res => {
                if (res.succeeded) {
                    localStorage.setItem(jwtPath, res.token);
                    localStorage.setItem(refreshPath, res.refreshToken);
                }
                this.signedIn = res.succeeded
            }).catch(error => {
                this.signedIn = false;
            });
        this.authenticating = false;
    }

    @action
    logout() {
        this.signedIn = false;
        localStorage.removeItem(jwtPath);
        localStorage.removeItem(refreshPath);

    }

    @action
    setRedirect(path: string) {
        console.log("AuthStore set redirect path: " + path)
        this.redirect = path;
    }

    @action
    consumeRedirect() {
        console.log("consumeRedirect value: " + this.redirect);
        if (this.redirect == null) {
            return this.redirect;
        }
        const res = this.redirect.valueOf();
        console.log("consumeRedirect.valueOf: " + res);
        this.redirect = null;;
        return res;
    }

    @action
    setSignedIn(isSignedIn: boolean) {
        this.signedIn = isSignedIn;
    }
}

export { AuthStore }