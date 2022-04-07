import { action, computed, makeAutoObservable, observable } from "mobx";
import { IStall, Stall } from "./Stall";

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
        id : number,
        organiserId : number,
        name : string,
        startDate : Date,
        endDate : Date,
        description : string,
        isCancelled : boolean,
        stalls : IStall[]
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

    @computed
    get stallCount()
    {
        return this.stalls.length;
    }

    //TODO: This is temporary (likely)
    @action
    setNumberOfStalls(count : number)
    {
        count = count < 0 ? 0 : count;
        const currentCount = this.stalls.length;
        const diff = count - currentCount;
        if(diff > 0)
        {
            var newStalls : IStall[] = [];
            for(var i = 0; i<diff; i++)
            {
                newStalls.push(new Stall())
            }
            this.stalls = this.stalls.concat(newStalls);
        }
        else if(diff < 0)
        {
            this.stalls = this.stalls.slice(0, diff);
        }
        else if(diff == 0)
        {
            this.stalls = [];
        }
    }
}