import { action, flow, makeAutoObservable, observable } from "mobx";
import { GetOrganiserQueryResponse, OrganiserBaseVM as Dto, OrganiserClient } from "../../services/clients";
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
     * Goes to the server and tries to grab the organiser.
     * Should probably just look through the locally stored organisers.
     * @param organiserId 
     */
    @action
    async resolveSelectedOrganiser(organiserId: number) {
        this.transportLayer.getOrganiser(organiserId + "")
            .then(
                action("fetchSuccess", result => {
                    const organiser = this.updateOrganiserFromServer(result.organiser);
                    organiser.select()
                    return organiser
                }),
                action("fetchFailed", error => {
                    throw error;
                })
            )
    }

    @flow
    *fetchOrganiser(organiserId: number) {
        const res: GetOrganiserQueryResponse = yield this.transportLayer.getOrganiser(organiserId + "");
        const organiser = this.updateOrganiserFromServer(res.organiser);
        return organiser
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
        console.log("fetching this frickadi fuckidy organiser")
        console.log(organiser)
        if (!organiser) {
            organiser = new Organiser(this);
            console.log("new organiser bitch")
            console.log(organiser)
            console.log(organiser.store)
            this.organisers.push(organiser);
        }
        organiser = organiser.updateFromServer(dto);
        return organiser
    }
}