import { action, makeAutoObservable, observable } from "mobx";

export interface IUser {
    id : string;
    firstname : string;
    lastname : string;
    email : string;
    phonenumber : string;
    dateOfBirth : Date;
    country : string;
    password : string;

    setId : (string) => void;
    setFirstName : (string) => void;
    setLastName : (string) => void;
    setEmail : (string) => void;
    setPhoneNumber : (string) => void;
    setDateOfBirth : (Date) => void;
    setCountry : (string) => void;
    setPassword : (string) => void;
    
}

export class User implements IUser {
    @observable id : string;
    @observable firstname : string;
    @observable lastname : string;
    @observable email : string;
    @observable phonenumber : string;
    @observable dateOfBirth : Date;
    @observable country: string;
    @observable password : string;

    constructor(id, 
        firstname : string, 
        lastname : string, 
        email : string, 
        phonenumber : string,
        dateOfBirth : Date,
        country : string,
        password : string ){
        makeAutoObservable(this);
        this.id = id;
        this.firstname = firstname;
        this.lastname = lastname;
        this.email = email;
        this.phonenumber = phonenumber;
        this.dateOfBirth = dateOfBirth;
        this.country = country;
        this.password = password;
    }

    @action
    setId(id : string){
        this.id = id;
    };
    
    @action
    setFirstName(firstName : string)
    {
        this.firstname = firstName;
    }
    
    @action
    setLastName(lastName : string){
        this.lastname = lastName;
    };
    
    @action
    setEmail(email : string)
    {
        this.email = email;
    }
    
    @action
    setPhoneNumber(phonenumber : string)
    {
        this.phonenumber = phonenumber;
    }
    
    @action
    setDateOfBirth(date : Date)
    {
        this.dateOfBirth = date;
    }
    
    @action
    setCountry(country : string)
    {
        this.country = country;
    }

    @action
    setPassword(password : string)
    {
        this.password = password;
    }
}