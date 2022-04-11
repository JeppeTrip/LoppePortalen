import { action, computed, observable } from "mobx"

export class Request {
    @observable done : boolean = false;
    @observable inProgress : boolean = false;
    @observable success : boolean = false;

    constructor(){
        this.reset();
    }

    @action
    reset(){
        this.done = false;
        this.inProgress = false;
        this.success = false;
    }

    @action
    onSubmit(){
        this.done = false;
        this.inProgress = true;
        this.success = false;
    }

    @action
    onFail(){
        this.done = true;
        this.inProgress = false;
        this.success = false;
    }

    @action
    onSuccess(){
        this.done = true;
        this.success = true;
        this.inProgress = false;
    }

    @computed
    get succeeded()
    {
        return this.done && this.success 
    }

    @computed
    get failed()
    {
        return this.done && !this.success
    }

    @computed
    get submitted()
    {
        return !this.done && this.inProgress
    }
}