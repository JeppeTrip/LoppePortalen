import { action, makeAutoObservable, observable, runInAction } from 'mobx';
import { RootStore } from '../RootStore';
import { Auth } from '../@DomainObjects/Auth'
import { AuthenticateUserRequest, AuthorizationClient } from '../../services/clients';

export class AuthStore {
    //Todo: maybe put these in the auth object?
    rootStore: RootStore
    transportLayer : AuthorizationClient

    @observable auth : Auth
    @observable isLoading : boolean = false;

    constructor(rootStore: RootStore, transportLayer : AuthorizationClient) {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
        this.auth = new Auth(this);
    }

    /**
     * Used for registering new users.
     * Gives a fresh user object with from the user store to the freshly spawned auth object.
     */
    
    @action 
    createAuth()
    {
        const auth = new Auth(this, this.rootStore.userStore.createUser());
        this.auth = auth;
        return auth;
    }
}