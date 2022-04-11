import { action, makeAutoObservable, observable } from 'mobx';
import { Market } from '../../@types/Market';
import { Organiser } from '../../@types/Organiser';
import { IUser, User } from '../../@types/User';
import { AuthenticateUserRequest, AuthorizationClient, MarketClient, OrganiserClient, RegisterUserRequest, RegisterUserResponse, UserClient } from '../models';
import { RootStore } from '../RootStore';

class UserStore {
    rootStore: RootStore;
    @observable currentUser: IUser;
    @observable oldUserData: IUser;
    @observable newUser: IUser;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        this.newUser = new User(undefined, "", "", "", "", null, "", "", [], [])
        this.currentUser = undefined
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
                        [],
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
                            [],
                            []
                        ))
                } else {

                }
            }).catch(error => {

            });
    }

    @action
    getUsersOrganisations(user: IUser) {
        const client = new OrganiserClient();
        client.getCurrentUsersOrganisers()
            .then(res => {
                const organisations = res.map(x => {
                    return (
                        new Organiser(
                            x.id,
                            x.name,
                            x.description,
                            x.street,
                            x.number,
                            x.appartment,
                            x.postalCode,
                            x.city
                        )
                    )
                })
                user.setOrganisations(organisations);
            }).catch(error => {
                //don't do much for now.
            });
    }

    @action
    getUsersMarkets(user: IUser) {
        const client = new MarketClient();
        client.getCurrentUsersMarkets()
            .then(res => {
                const markets = res.markets.map(x => {
                    return (
                        new Market(
                            x.marketId,
                            x.organiserId,
                            x.marketName,
                            new Date(x.startDate),
                            new Date(x.endDate),
                            x.description,
                            x.isCancelled,
                            []
                        )
                    )
                })
                user.setMarkets(markets);
            }).catch(error => {
                //don't do much for now.
            });
    }

    @action
    resetNewUser() {
        this.newUser = new User(undefined, "", "", "", "", null, "", "", [], []);
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
    resetCurrentUser() {
        const data = new User(
            this.oldUserData.id,
            this.oldUserData.firstname,
            this.oldUserData.lastname,
            this.oldUserData.email,
            this.oldUserData.phonenumber,
            this.oldUserData.dateOfBirth,
            this.oldUserData.country,
            this.oldUserData.password,
            this.oldUserData.organisations,
            this.oldUserData.markets
        )
        this.setCurrentUser(data);
    }
}

export { UserStore }