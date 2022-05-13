import { action, makeAutoObservable, observable } from "mobx";
import { ModelState } from "../../@types/ModelState";
import { ContactInfoType } from "../../services/clients";
import { Organiser } from "./Organiser";

export class ContactInfo {
    @observable value : string
    @observable type : ContactInfoType
    @observable state : ModelState

    set Value(value : string) { this.value = value }
    set Type(type : ContactInfoType) {this.type = type }
    set State(state : ModelState) {this.state = state}
    constructor(){
        makeAutoObservable(this)
        this.value = ""
        this.type = ContactInfoType.PHONE_NUMER
        this.state = ModelState.NEW
    }
}