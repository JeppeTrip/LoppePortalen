import { action, makeAutoObservable, observable } from "mobx";
import { IStall } from "./Stall";

export interface IMarket {
    id : number;
    organiserId : number;
    name : string;
    startDate : Date;
    endDate : Date;
    description : string;
    isCancelled : boolean;
    stalls: IStall[]
}

export class Market implements IMarket {
    @observable id : number;
    @observable organiserId : number;
    @observable name : string;
    @observable startDate : Date;
    @observable endDate : Date;
    @observable description : string;
    @observable isCancelled : boolean;
    @observable stalls: IStall[]

    constructor(
        id,
        organiserId,
        name,
        startDate,
        endDate,
        description,
        isCancelled,
        stalls
    ) {
        makeAutoObservable(this)
        this.id = id,
        this.organiserId = organiserId,
        this.name = name,
        this.startDate = startDate,
        this.endDate = endDate,
        this.description = description,
        this.isCancelled = isCancelled,
        this.stalls = stalls
    }

    @action
    setId(id : number)
    {
        this.id = id;
    }

    @action
    setOrganiserId(id : number)
    {
        this.organiserId = id;
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
    setIsCancelled(isCancelled : boolean)
    {
        this.isCancelled = isCancelled;
    }

    @action
    setStalls(stalls : IStall[])
    {
        this.stalls = stalls;
    }

}