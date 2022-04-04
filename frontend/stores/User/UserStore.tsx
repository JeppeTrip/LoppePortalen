import { makeAutoObservable } from 'mobx';
import { ClientStore } from '../../services/Clients';
import { AuthenticateUserRequest, AuthorizationClient } from '../models';
import { RootStore } from '../RootStore';

class UserStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    
    login(email : string, password : string){
        const client = new AuthorizationClient();
        const request = {    
            email: email,
            password: password 
        } as AuthenticateUserRequest;
        client.authenticateUser(request)
        .then(res => {
            if(res.succeeded)
            {
                localStorage.setItem("user", res.token);
            }
            else {
                console.log(`Authentication fail: ${email}, ${password}`);
            }
        })
    }
}

export { UserStore }