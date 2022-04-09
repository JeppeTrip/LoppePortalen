import { action, makeAutoObservable } from 'mobx';
import { IUser, User } from '../../@types/User';
import { AuthenticateUserRequest, AuthorizationClient, RegisterUserRequest, RegisterUserResponse } from '../models';
import { RootStore } from '../RootStore';

class UserStore {
    rootStore: RootStore;
    newUser: IUser;


    hadAuthenticationError: boolean = false;
    //Move UI state out of this store.
    isLoggingIn: boolean = false;
    isLoggedIn: boolean = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        this.newUser = new User("", "", "", "", "", null, "", "")
    }

    @action
    login(email: string, password: string) {
        this.setIsLoggingIn(true);
        this.setHadAuthenticationError(false);
        const client = new AuthorizationClient();
        const request = {
            email: email,
            password: password
        } as AuthenticateUserRequest;
        client.authenticateUser(request)
            .then(res => {
                if (res.succeeded) {
                    localStorage.setItem("user", res.token);
                    this.setIsLoggingIn(false);
                    this.setIsLoggedIn(true);
                }
                else {
                    this.setHadAuthenticationError(true);
                    this.setIsLoggingIn(false);
                    this.setIsLoggedIn(false);
                }
            }).catch(error => {
                this.setHadAuthenticationError(true);
                this.setIsLoggingIn(false);
                this.setIsLoggedIn(false);
            });
    }

    @action
    async registerUser() {
        this.rootStore.userFormUiStore.beginSubmit()
        const client = new AuthorizationClient();
        const request = {
            email: this.newUser.email,
            password: this.newUser.password,
            firstname: this.newUser.firstname,
            lastname: this.newUser.lastname,
            phonenumber: this.newUser.phonenumber,
            dateOfBirth: this.newUser.dateOfBirth,
            country: this.newUser.country

        } as RegisterUserRequest;
        client.registerUser(request)
            .then(res => {
                if (res.succeeded) {

                    this.newUser.id = res.id;
                    this.rootStore.userFormUiStore.submitSuccess()
                } else {
                    this.rootStore.userFormUiStore.hadSubmissionError()
                }
            }).catch(error => {
                this.rootStore.userFormUiStore.hadSubmissionError()
            });
    }

    @action
    resetNewUser() {
        this.newUser = new User("", "", "", "", "", null, "", "")
    }

    @action
    logout() {
        localStorage.removeItem("user");
        this.setIsLoggedIn(false);
    }

    @action
    setHadAuthenticationError(hadError: boolean) {
        this.hadAuthenticationError = hadError;
    }

    @action
    setIsLoggingIn(isLoggingIn: boolean) {
        this.isLoggingIn = isLoggingIn;
    }

    @action
    setIsLoggedIn(isLoggedIn: boolean) {
        this.isLoggedIn = isLoggedIn;
    }
}

export { UserStore }