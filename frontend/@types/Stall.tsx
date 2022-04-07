import { action, makeObservable, observable } from "mobx";

export interface IStall {
    id : number;
    type : string;
    description : string;

    setDescription;
    setType;
}

export class Stall implements IStall {
    id : number;
    @observable type : string;
    @observable description : string;

    constructor(type : string, description : string, id? : number){
        makeObservable(this);
        this.type = type;
        this.description = description;
    }

    @action
    setType(type : string)
    {
        this.type = type;
    }

    @action
    setDescription(description : string)
    {
        this.description = description;
    }
}