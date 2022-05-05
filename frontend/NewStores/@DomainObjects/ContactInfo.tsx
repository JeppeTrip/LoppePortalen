import { action, makeAutoObservable, observable } from "mobx";
import { ModelState } from "../../@types/ModelState";
import { ContactInfoType } from "../../services/clients";
import { Organiser } from "./Organiser";

export class ContactInfo {
    @observable organiser : Organiser
    @observable value : string
    @observable type : ContactInfoType
    @observable state : ModelState

    constructor(organiser : Organiser){
        makeAutoObservable(this)
        this.organiser = organiser
        this.value = ""
        this.type = ContactInfoType.PHONE_NUMER
        this.state = ModelState.NEW
    }

    @action 
    addToOrganiser()
    {
        this.organiser.addContactInfo(this)
    }

    @action
    delete()
    {
        this.state = ModelState.EDITING
        this.organiser.deleteContactInfo(this)
    }
}