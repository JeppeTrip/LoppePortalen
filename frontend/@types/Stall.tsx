import { observable } from "mobx";

export interface IStall {
    id : number;
    type : string;
    description : string;
}

export class Stall implements IStall {
    id : number;
    @observable type : string;
    @observable description : string;

    constructor(type : string, description : string){
        this.type = type;
        this.description = description;
    }
}