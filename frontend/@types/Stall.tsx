export interface IStall {
    id : number;
    type : string;
    description : string;
}

export class Stall implements IStall {
    id : number;
    type : string;
    description : string;

    constructor()
    {

    }
}