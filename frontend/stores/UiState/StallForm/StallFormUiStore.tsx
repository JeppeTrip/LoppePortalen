import { action, makeAutoObservable, observable } from 'mobx';
import { RootStore } from '../../RootStore';

class StallFormUiStore {
    rootStore: RootStore;

    @observable isAddingNewStall : boolean = false;
    @observable isInvalidStall : boolean = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    @action
    resetState()
    {
        this.isAddingNewStall = false;
        this.isInvalidStall = false;
    }

    @action
    addingStall()
    {
        this.isAddingNewStall = true;
        this.isInvalidStall = false;
    }

    @action
    setIsInvalidStall(isInvalidStall : boolean)
    {
        this.isInvalidStall = isInvalidStall;
    }

    @action
    setIsAddingNewStall(isAddingNewStall : boolean)
    {
        this.isAddingNewStall = isAddingNewStall;
    }

}

export { StallFormUiStore }