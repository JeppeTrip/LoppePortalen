//Represent the state domain objects can be in, to get some more control 
//over when what happens.

export class ModelState {
    static IDLE = Symbol("idle")
    static NEW = Symbol("new")
    static UPDATING = Symbol("updating")
    static EDITING = Symbol("editing")
}
