import { action, makeAutoObservable, observable } from 'mobx';
import { IUser, User } from '../../@types/User';
import { AuthenticateUserRequest, AuthorizationClient, RegisterUserRequest, RegisterUserResponse, UserClient } from '../models';
import { RootStore } from '../RootStore';

class UserStore {
    rootStore: RootStore;
    @observable currentUser: IUser;
    @observable oldUserData: IUser;
    @observable newUser: IUser;


    hadAuthenticationError: boolean = false;
    //Move UI state out of this store.
    isLoggingIn: boolean = false;
    isLoggedIn: boolean = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        this.newUser = new User("", "", "", "", "", null, "", "", [])
        this.currentUser = new User("", "", "", "", "", null, "", "", [])
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
                    this.rootStore.userFormUiStore.submitSuccess()
                } else {
                    this.rootStore.userFormUiStore.hadSubmissionError()
                }
            }).catch(error => {
                this.rootStore.userFormUiStore.hadSubmissionError()
            });
    }

    @action
    getCurrentUser() {
        const client = new UserClient();
        client.getUserInfo()
            .then(res => {
                if (res.succeeded) {
                    this.setCurrentUser(new User(
                        res.id,
                        res.firstName,
                        res.lastName,
                        res.email,
                        res.phoneNumber,
                        res.dateOfBirth,
                        res.country,
                        "",
                        []
                    ))
                    this.setOldUser(
                        new User(
                            res.id,
                            res.firstName,
                            res.lastName,
                            res.email,
                            res.phoneNumber,
                            res.dateOfBirth,
                            res.country,
                            "",
                            []
                        ))
                } else {

                }
            }).catch(error => {

            });
    }

    @action
    resetNewUser() {
        this.newUser = new User("", "", "", "", "", null, "", "", [])
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

    @action
    setCurrentUser(user: IUser) {
        this.currentUser = user;
    }

    @action
    setOldUser(user: IUser) {
        this.oldUserData = user;
    }

    @action
    resetCurrentUser()
    {
        const data = new User(
            this.oldUserData.id,
            this.oldUserData.firstname,
            this.oldUserData.lastname,
            this.oldUserData.email,
            this.oldUserData.phonenumber,
            this.oldUserData.dateOfBirth,
            this.oldUserData.country,
            this.oldUserData.password,
            this.oldUserData.organisations
        )
        this.setCurrentUser(data);
    }
}

export { UserStore }