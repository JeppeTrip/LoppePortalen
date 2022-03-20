export interface IOrganiser {
    id : number;
    name : string;
    description : string;
    street : string;
    streetNumber : string;
    appartment: string;
    postalCode : string;
    city : string
}

export type OrganiserContextType = {
    organisers: IOrganiser[];
    addOrganiser : (organiser : IOrganiser) => void;
    fetchAllOrganisers : () => void;
}