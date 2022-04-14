import { action, flowResult, makeAutoObservable, observable } from "mobx";
import { AuthStore } from "../stores/AuthStore";
import { User } from "./User";

/**
 * Container class for what I would need in terms of authorization jazz.
 * It will also hold a userinfo reference, just so it is easier to load in user 
 * data as part of the auth process.
 */
export class Auth {
    authStore: AuthStore
    jwtPath: string = "loppeportalen_jwt"
    refreshPath: string = "loppeportalen_refresh"
    @observable email: string = "" //email to be used for authentication.
    @observable password: string = "" //password to be used for authentication.

    @observable user: User = null
    @observable signedIn: boolean = false;
    @observable initializing: boolean = true;

    constructor(authStore: AuthStore, user?: User) {
        makeAutoObservable(this);
        this.authStore = authStore
        this.user = user;
    }

    @action
    signIn() {
        this.authStore.transportLayer
            .authenticateUser({
                email: this.email,
                password: this.password
            }).then(
                action("authSuccess", res => {
                    localStorage.setItem(this.jwtPath, res.token);
                    localStorage.setItem(this.refreshPath, res.refreshToken);
                    
                    this.signedIn = true
                }),
                action("authFailed", error => {
                    this.email = ""
                    this.password = ""
                    this.signedIn = false;
                })
            )
    }

    /**
    * Register new user with this method.
    * Take both the auth data and user info data and put it in with this.
    */
    @action
    registerUser() {
        this.authStore.isLoading = true;
        this.authStore.transportLayer.registerUser({
            email: this.email,
            password: this.password,
            firstName: this.user.firstName,
            lastName: this.user.lastName,
            dateOfBirth: this.user.dateOfBirth,
            phoneNumber: this.user.phoneNumber,
            country: this.user.country
        }).then(
            action("registerUserSuccess", res => {
                if (res.succeeded) {
                    localStorage.setItem(this.jwtPath, res.token);
                    localStorage.setItem(this.refreshPath, res.refreshToken);
                }
                this.signedIn = res.succeeded;
                this.authStore.isLoading = false;
            }),
            action("registerUserFailed", res => {
                if (res.succeeded) {
                    localStorage.setItem(this.jwtPath, res.token);
                    localStorage.setItem(this.refreshPath, res.refreshToken);
                }
                this.signedIn = res.succeeded;
                this.authStore.isLoading = false;
            })
        )
    }

    @action
    public initialize() {
        console.log("initializing")
        this.initializing = true;
        var key = localStorage.getItem(this.jwtPath)

        /**
         * If there's already a key in storage we are signed in (naively).
         * Otherwises we are not.
         */
        if (key != null) {
            console.log("Resolving user.")
            flowResult(this.authStore.rootStore.userStore.resolveCurrentUser())
                .then(
                    action("resolvedUserSuccess", user => {
                        console.log("resolve success")
                        console.log(user)
                        if (user != null) {
                            this.user = user
                            this.signedIn = true;
                        }
                        else {
                            this.user = null
                            this.signedIn = false;
                        }
                        this.initializing = false;
                    }),
                    action("resolvedUserFailed", user => {
                        this.user = null
                        this.signedIn = false;
                        this.initializing = false;
                    })
                );
        }
    }

    set setEmail(email: string) {
        this.email = email;
    }

    set setPassword(password: string) {
        this.password = password;
    }
}