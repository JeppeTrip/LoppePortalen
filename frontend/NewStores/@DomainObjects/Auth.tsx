import { action, flowResult, makeAutoObservable, observable } from "mobx";
import { AuthenticateUserRequest, RegisterUserRequest } from "../../services/clients";
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

    set Email(email: string) { this.email = email }
    set Password(password: string) { this.password = password }

    constructor(authStore: AuthStore, user?: User) {
        makeAutoObservable(this);
        this.authStore = authStore
        this.user = user;
    }

    @action
    signIn() {
        this.authStore.transportLayer
            .authenticateUser(new AuthenticateUserRequest({
                email: this.email,
                password: this.password
            })).then(
                action("authSuccess", res => {
                    localStorage.setItem(this.jwtPath, res.token);
                    localStorage.setItem(this.refreshPath, res.refreshToken);
                    flowResult(this.authStore.rootStore.userStore.resolveCurrentUser())
                        .then(
                            action("resolvedUserSuccess", user => {
                                if (user != null) {
                                    this.user = user
                                    this.signedIn = true;
                                }
                                else {
                                    this.user = null
                                    this.signedIn = false;
                                }
                            }),
                            action("resolvedUserFailed", user => {
                                this.user = null
                                this.signedIn = false;
                            })
                        )
                    action("authFailed", error => {
                        this.email = ""
                        this.password = ""
                        this.signedIn = false;
                    })
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
        this.authStore.transportLayer.registerUser(new RegisterUserRequest({
            email: this.email,
            password: this.password,
            firstName: this.user.firstName,
            lastName: this.user.lastName,
            dateOfBirth: this.user.dateOfBirth,
            phoneNumber: this.user.phoneNumber,
            country: this.user.country
        })).then(
            action("registerUserSuccess", res => {
                if (res.succeeded) {
                    localStorage.setItem(this.jwtPath, res.token);
                    localStorage.setItem(this.refreshPath, res.refreshToken);
                    flowResult(this.authStore.rootStore.userStore.resolveCurrentUser())
                        .then(
                            action("resolvedUserSuccess", user => {
                                if (user != null) {
                                    this.user = user
                                    this.signedIn = true;
                                }
                                else {
                                    this.user = null
                                    this.signedIn = false;
                                }
                            }),
                            action("resolvedUserFailed", user => {
                                this.user = null
                                this.signedIn = false;
                            })
                        )
                }
                this.signedIn = res.succeeded;
                this.authStore.isLoading = false;
            }),
            action("registerUserFailed", res => {
                if (res.succeeded) {
                    localStorage.removeItem(this.jwtPath);
                    localStorage.removeItem(this.refreshPath);
                }
                this.signedIn = res.succeeded;
                this.authStore.isLoading = false;
            })
        )
    }

    @action
    public initialize() {
        this.initializing = true;
        var key = localStorage.getItem(this.jwtPath)

        /**
         * If there's already a key in storage we are signed in (naively).
         * Otherwises we are not.
         */
        if (key != null) {
            flowResult(this.authStore.rootStore.userStore.resolveCurrentUser())
                .then(
                    action("resolvedUserSuccess", user => {
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

    @action
    logOut(){
        this.email = ""
        this.password = ""
        localStorage.removeItem(this.jwtPath);
        localStorage.removeItem(this.refreshPath);
        this.signedIn = false;
    }
}