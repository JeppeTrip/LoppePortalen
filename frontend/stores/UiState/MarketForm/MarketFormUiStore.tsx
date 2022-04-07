import { action, makeAutoObservable } from 'mobx';
import { IOrganiser } from '../../../@types/Organiser';
import { RootStore } from '../../RootStore';

class MarketFormUiStore {
    rootStore: RootStore;

    isSubmittingForm : boolean = false;


    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
    }

    @action
    setName(name : string)
    {
        this.name = name;
    }

    @action
    setStartDate(date : Date)
    {
        this.startDate = date;
    }

    @action
    setEndDate(date : Date)
    {
        this.endDate = date;
    }

    @action
    setDescription(description : string)
    {
        this.description = description;
    }

    @action
    setNumberOfStalls(count : number)
    {
        this.numberOfStalls = count;
    }

    @action
    resetForm()
    {
        this.selectedOrganiser = null;
        this.name= "";
        this.startDate= new Date();
        this.endDate= new Date();
        this.description = "" ;
        this.numberOfStalls = 0;
        this.isSubmittingForm = false;
    }

    @action
    setIsSubmittingForm(isSubmitting : boolean)
    {
        this.isSubmittingForm = isSubmitting;
    }
  
}

export { MarketFormUiStore }