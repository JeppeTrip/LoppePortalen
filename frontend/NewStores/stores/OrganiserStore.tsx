import { action, flow, flowResult, makeAutoObservable } from "mobx";
import { OrganiserClient, Organiser as dto } from "../../stores/models";
import { Organiser } from "../@DomainObjects/Organiser";
import { User } from "../@DomainObjects/User";
import { RootStore } from "../RootStore";

export class OrganiserStore {
    rootStore: RootStore
    transportLayer: OrganiserClient
    organisers: Organiser[]

    constructor(rootStore: RootStore, transportLayer: OrganiserClient) {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
    }

    /**
    * Tries to fetch all organisations from the backend.
    * Force updates the entire organiser list.
    * TODO: Ensure that all the organisers in this list also removes it from the listening objects.
    */
    @flow
    *resolveOrganisersAll() {
        console.log("resolve all organisers")
        try {
            const result = yield this.transportLayer.getAllOrganisers()
            console.log(result)
            const organisers = result.organisers.map(x => {
                const organiser = new Organiser(this, x.id, x.userId)
                organiser.name = x.name
                organiser.description = x.description
                organiser.street = x.street
                organiser.streetNumber = x.streetNumber
                organiser.appartment = x.appartment
                organiser.postalCode = x.postalCode
                organiser.city = x.city

                return organiser
            });
            console.log(organisers)
            console.log("post fetch organisers")
            console.log(organisers)
            this.organisers = organisers;
        }
        catch (error) {
            this.organisers = []
        }
        finally {
            return this.organisers
        }
    }

    /**
    * Tries to fetch organisations from the backend that satisfies the constraints put on the by the given filters..
    * TODO: Consider if this should invalidate the entire organiser list or just update with the newly aquired results.
    */
    @flow
    *resolveOrganisersFiltered(userId?: string) {
        console.log("TODO: UPDATE THE ORGANISATIONS FILTER TO FILTER IN THE BACKEND!")

        try{
            const allOrgs = yield flowResult(this.resolveOrganisersAll())
        } 
        catch(error){

        }
        finally{
            return this.organisers.filter(x => x.userId === userId)
        }
    }
}