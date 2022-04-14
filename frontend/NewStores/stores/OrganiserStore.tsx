import { action, flow, flowResult, makeAutoObservable, observable } from "mobx";
import { OrganiserClient, Organiser as dto } from "../../stores/models";
import { Organiser } from "../@DomainObjects/Organiser";
import { User } from "../@DomainObjects/User";
import { RootStore } from "../RootStore";
import { Organiser as Dto } from "../../stores/models";

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
    @action
    fetchAllOrganisers() {
        console.log("fetch all organisers")
        this.transportLayer.getAllOrganisers()
        .then(
            action("fetchSuccess", result => {
                console.log("fetchResult\n")
                console.log(result)
                result.organisers.forEach(dto => {
                    console.log("iterate through organisers")
                    console.log(dto)
                    this.updateOrganiserFromServer(dto)
                });
            }),
            action("fetchError", error => {
                this.organisers = []
            })
        )
    }

    /**
    * Tries to fetch organisations from the backend that satisfies the constraints put on the by the given filters..
    * TODO: Consider if this should invalidate the entire organiser list or just update with the newly aquired results.
    */
    @flow
    *resolveOrganisersFiltered(userId?: string) {
        console.log("TODO: UPDATE THE ORGANISATIONS FILTER TO FILTER IN THE BACKEND!")
        try {
            const allOrgs = yield flowResult(this.fetchAllOrganisers())
        }
        catch (error) {

        }
        finally {
            return this.organisers.filter(x => x.userId === userId)
        }
    }

    @action
    resolveSelectedOrganiser(organiserId: number) {
        flowResult(this.fetchAllOrganisers())
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

    @action
    createOrganiser() {
        const organiser = new Organiser(this);
        this.selectedOrganiser = organiser;
        return this.selectedOrganiser;
    }

    @action
    updateOrganiserFromServer(dto: Dto) {
        console.log("update organiser from server")
        console.log(dto)
        let organiser = this.organisers.find(x => x.id === dto.id);
        console.log(organiser)
        if (!organiser) {
            organiser = new Organiser(this);
            this.organisers.push(organiser);
        }
        return organiser.updateFromServer(dto);
    }
}