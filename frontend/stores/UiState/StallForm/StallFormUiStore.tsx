import { action, makeAutoObservable, observable } from 'mobx';
import { RootStore } from '../../RootStore';

class StallFormUiStore {
    rootStore: RootStore;

    @observable isAddingNewStall : boolean = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    @action
    setIsAddingNewStall(isAddingNewStall : boolean)
    {
        this.isAddingNewStall = isAddingNewStall;
    }

}

export { StallFormUiStore }