import { action, flow, flowResult, makeAutoObservable, observable } from "mobx";
import { OrganiserClient, Organiser as dto } from "../../stores/models";
import { Organiser } from "../@DomainObjects/Organiser";
import { User } from "../@DomainObjects/User";
import { RootStore } from "../RootStore";

export class OrganiserStore {
    rootStore: RootStore
    transportLayer: OrganiserClient
    @observable organisers: Organiser[]
    @observable selectedOrganiser: Organiser

    constructor(rootStore: RootStore, transportLayer: OrganiserClient) {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
        this.organisers = []
        this.selectedOrganiser = null
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
            console.log("fetchResult\n")
            console.log(result)
            const organisers = result.organisers.map(x => {
                console.log(x)
                const organiser = new Organiser(this)
                organiser.update(x)
                return organiser
            });
            console.log("organisers\n"+organisers)
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
        try {
            const allOrgs = yield flowResult(this.resolveOrganisersAll())
        }
        catch (error) {

        }
        finally {
            return this.organisers.filter(x => x.userId === userId)
        }
    }

    @action
    resolveSelectedOrganiser(organiserId: number) {
        flowResult(this.resolveOrganisersAll())
            .then(
                action("fetchSuccess", result => {
                    console.log(result)
                    const organiser = result.find(x => x.id === organiserId);
                    console.log("find the one I'm looking for")
                    console.log(organiser)
                    if (!organiser) {
                        return null;
                    }
                    else {
                        organiser.select()
                        return organiser
                    }
                }),
                action("fetchFailed", error => {
                    return null;
                })
            )
    }
}