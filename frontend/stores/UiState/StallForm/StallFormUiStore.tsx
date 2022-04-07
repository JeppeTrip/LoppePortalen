import { action, makeAutoObservable, observable } from 'mobx';
import { RootStore } from '../../RootStore';

class StallFormUiStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

}

export { StallFormUiStore }