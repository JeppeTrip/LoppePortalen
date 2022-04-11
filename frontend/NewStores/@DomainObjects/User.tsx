import { makeAutoObservable, observable } from "mobx";
import { RootStore } from "../../stores/RootStore";

export class User {
    rootStore : RootStore = null
    id : string = null
    @observable firstName : string = ""
    @observable lastName : string = ""
    @observable email : string = "" //get this from the auth object?
    @observable phoneNumber : string = ""
    @observable dateOfBirth : Date = null
    @observable country : string = ""
     //markets from a market store
     //organisers form a organiser store

     constructor(rootStore, id? : string)
     {
         makeAutoObservable(this)
         this.rootStore = rootStore
         this.id = id
     }

     set setFirstName(name : string){
        this.firstName = name
     } 

     set setLastName(name : string)
     {
         this.lastName = name
     }

     set setEamil(email : string)
     {
         this.email = email;
     }

     set setPhoneNumber(phoneNumber : string)
     {
         this.phoneNumber = phoneNumber;
     }

     set setDateOfBirth(date : Date)
     {
        this.dateOfBirth = date;
     }

     set setCountry(country : string)
     {
         this.country = country
     }
}