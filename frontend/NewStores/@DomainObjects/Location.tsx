import { makeAutoObservable, observable } from "mobx"

export class Location {
    @observable address : string
    @observable postalCode : string
    @observable city : string
    @observable x : number
    @observable y : number

    constructor(){
        makeAutoObservable(this)
        this.address = ""
        this.postalCode = ""
        this.city = ""
    }
}