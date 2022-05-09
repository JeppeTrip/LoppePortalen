import { action, makeAutoObservable, observable } from "mobx";
import { ModelState } from "../../@types/ModelState";
import { ContactInfoType } from "../../services/clients";
import { Organiser } from "./Organiser";

export class ContactInfo {
    @observable value : string
    @observable type : ContactInfoType
    @observable state : ModelState

    constructor(){
        makeAutoObservable(this)
        this.value = ""
        this.type = ContactInfoType.PHONE_NUMER
        this.state = ModelState.NEW
    }
}