import { makeAutoObservable, observable } from "mobx";


export class Stall{
    @observable id
    @observable name : string = ""
    @observable description : string = ""

    constructor()
    {
        makeAutoObservable(this)
    }

    set setName(name : string)
    {
        this.name = name
    }

    set setDescription(description)
    {
        this.description = description
    }
}