import { action, computed, makeAutoObservable, observable } from "mobx";
import { IStall, Stall } from "./Stall";

export class Market{
    @observable id : number;
    @observable organiserId : number;
    @observable name : string;
    @observable startDate : Date;
    @observable endDate : Date;
    @observable description : string;
    @observable isCancelled : boolean;
    @observable stalls: IStall[];

    @observable newStall: IStall;

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
    stallCount(type : string)
    {
        return this.stalls.filter(x => x.type === type).length;
    }

    @computed
    get stallCounts()
    {
        var unique = this.uniqueStalls();
        var stallCounts = unique.map(stall => [stall, this.stalls.filter(x => x.type === stall.type).length])
        return stallCounts;
    }

    @computed
    uniqueStalls(){
        var result = []
        var stallTypeSet = new Set<string>(this.stalls.map(x => x.type));
        stallTypeSet.forEach(element => {
            result.push(this.stalls.find(x => x.type == element))
        });
        return result;
    }

    //TODO: This is temporary (likely)
    @action
    setNumberOfStalls(type : string, count : number)
    {
        var unique = this.stalls.filter(x => x.type === type);
        var stall = unique[0];

        if(stall === null ||stall === undefined)
        {
            return;
        }
        count = count < 1 ? 1 : count;
        const currentCount = unique.length;
        const diff = count - currentCount;

        if(diff > 0)
        {
            var newStalls : IStall[] = [];
            for(var i = 0; i<diff; i++)
            {
                newStalls.push(stall)
            }
            this.stalls = this.stalls.concat(newStalls);
        }
        else if(diff < 0)
        {
            this.stalls = this.stalls.slice(0, diff);
        }
    }

    @action
    setNewStall(){
        this.newStall = new Stall("", "");
    }

    @action
    addStall(stall : IStall)
    {
        var filtered = this.stalls.filter(x => x.type === stall.type);
        if(filtered.length != 0 || stall.type.length === 0 || stall.description.length === 0)
        {
            return false;
        }
        this.stalls.push(stall)
        return true;
    }
}