import { ActionTypes } from "@mui/base";
import { action, makeAutoObservable, observable } from "mobx";

export interface IOrganiser {
    id : number;
    name : string;
    description : string;
    street : string;
    streetNumber : string;
    appartment: string;
    postalCode : string;
    city : string

    setId: (id : number) => void;
    setName: (name : string) => void;
    setDescription: (description : string) => void;
    setStreet: (street : string) => void;
    setStreetNumber: (streetNumber : string) => void;
    setAppartment: (appartment : string) => void;
    setPostalCode: (postalCode : string) => void;
    setCity: (city : string) => void;
}

export class Organiser implements IOrganiser {
    @observable id : number;
    @observable name : string;
    @observable description : string;
    @observable street : string;
    @observable streetNumber : string;
    @observable appartment : string;
    @observable postalCode : string;
    @observable city : string;

    constructor(id, name, description, street, streetNumber, appartment, postalCode, city)
    {
        makeAutoObservable(this);
        this.id = id;
        this.name = name;
        this.description = description;
        this.street = street; 
        this.streetNumber = streetNumber;
        this.appartment = appartment;
        this.postalCode = postalCode;
        this.city = city;
    }

    @action
    setId = (id : number ) => this.id = id;
    @action
    setName = (name : string) => this.name = name
    @action
    setDescription = (description : string) => this.description = description;
    @action
    setStreet = (street : string) => this.street = street;
    @action
    setStreetNumber = (streetNumber: string) => this.streetNumber = streetNumber;
    @action
    setAppartment = (appartment: string) => this.appartment = appartment;
    @action
    setPostalCode = (postalCode: string) => this.postalCode = postalCode;
    @action
    setCity = (city : string) => this.city = city
}