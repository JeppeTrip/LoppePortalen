import { action, flow, flowResult, makeAutoObservable, observable } from "mobx";
import { Organiser as Dto, OrganiserClient } from "../../services/clients";
import { Organiser } from "../@DomainObjects/Organiser";
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
    @action
    fetchAllOrganisers() {
        this.transportLayer.getAllOrganisers()
        .then(
            action("fetchSuccess", result => {
                result.organisers.forEach(dto => {
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
        this.transportLayer.getOrganiser(organiserId+"")
            .then(
                action("fetchSuccess", result => {
                    let organiser = this.updateOrganiserFromServer(result.organiser);
                    organiser.select()
                }),
                action("fetchFailed", error => {
                    //do somehting with this
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
        let organiser = this.organisers.find(x => x.id === dto.id);
        if (!organiser) {
            organiser = new Organiser(this);
            this.organisers.push(organiser);
        }
        return organiser.updateFromServer(dto);
    }
}