import { action, makeAutoObservable } from 'mobx';
import { RootStore } from '../RootStore';

class UiStateStore {
    rootStore: RootStore;
    drawerWidth = 240;
    isDrawerOpen = true;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    @action
    toggleDrawer() {
        this.isDrawerOpen = !this.isDrawerOpen;
    }

}

export { UiStateStore }