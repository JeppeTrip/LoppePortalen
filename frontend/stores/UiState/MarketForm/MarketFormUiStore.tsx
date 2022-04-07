import { action, makeAutoObservable, observable } from 'mobx';
import { IOrganiser } from '../../../@types/Organiser';
import { RootStore } from '../../RootStore';

class MarketFormUiStore {
    rootStore: RootStore;

    @observable isSubmittingForm : boolean = false;
    @observable showError : boolean = false;
    @observable redirect : boolean = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    @action
    resetState()
    {
        console.log("resetState")
        this.isSubmittingForm = false;
        this.showError = false;
        this.redirect = false;
    }

    @action
    beginSubmit()
    {
        console.log("beginSubmit")
        this.isSubmittingForm = true;
        this.showError = false;
        this.redirect = false;
    }

    @action
    hadSubmissionError()
    {
        console.log("hadError")
        this.isSubmittingForm = false;
        this.showError = true;
        this.redirect = false;
    }

    @action
    submitSuccess()
    {
        console.log("success")
        this.isSubmittingForm = false;
        this.showError = false;
        this.redirect = true;
    }
  
}

export { MarketFormUiStore }