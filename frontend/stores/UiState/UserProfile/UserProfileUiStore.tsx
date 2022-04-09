import { action, computed, makeAutoObservable, observable } from "mobx";
import { isDeepStrictEqual } from "util";
import { RootStore } from "../../RootStore";

const util = require('util');

class UserProfileUiStore {
    rootStore: RootStore;

    @observable isUserChanged = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    @action
    resetState() {
        this.isUserChanged = false;
    }

    @computed
    setIsUserChanged(isChanged : boolean) {
        return this.isUserChanged = isChanged;
    }

}

export { UserProfileUiStore }