import { action, makeAutoObservable, observable } from 'mobx';
import { IOrganiser, Organiser } from '../../@types/Organiser';
import { OrganiserClient } from '../models';
import { RootStore } from '../RootStore';

class OrganiserStore {
    rootStore: RootStore;
    @observable organisers: IOrganiser[] = [];
    @observable newOrganiser: IOrganiser;
    @observable selectedOrganiser: IOrganiser | null = null;
    @observable editedOrganiser: IOrganiser;

    @observable isLoading = true;
    @observable hadLoadingError = false;

    @observable isSubmitting = false;
    @observable hadSubmissionError = false;
    @observable submitSuccess = true;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.newOrganiser = new Organiser(
            undefined,
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        )
        this.rootStore = rootStore;
    }

    @action
    loadOrganisers() {
        this.hadLoadingError = false;
        this.isLoading = true;
        const client = new OrganiserClient();
        client.getAllOrganisers().then(res => {
            var result = res.map<IOrganiser>(org => {
                return (
                    new Organiser(
                        org.id,
                        org.name,
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""
                    )
                )
            })
            this.organisers = result
            this.isLoading = false;
            this.hadLoadingError = false;
        }).catch(error => {
            this.isLoading = false;
            this.hadLoadingError = true;
        })
    }



    @action
    //TODO: Don't know that it is a good idea to add new organiser here as it becomes dependent on state in the user store.
    addOrganiser(organiser: IOrganiser) {
        this.isSubmitting = true;
        const client = new OrganiserClient();
        this.newOrganiser = organiser

        client.createOrganiser({
            userId: this.rootStore.userStore.currentUser.id,
            name: organiser.name,
            description: organiser.description,
            street: organiser.street,
            number: organiser.streetNumber,
            appartment: organiser.appartment,
            postalCode: organiser.postalCode,
            city: organiser.city
        }).then(res => {
            this.newOrganiser.id = res.id
            this.organisers.push(this.newOrganiser);
            this.isSubmitting = false;
            this.submitSuccess = true;
        }).catch(error => {
            this.submitSuccess = false;
            this.hadSubmissionError = true;
            this.isSubmitting = false;
            this.newOrganiser.id = -1;
        })
    }

    @action
    loadOrganiser(organiserId: number) {
        this.setIsLoading(true);

        const client = new OrganiserClient();
        client.getOrganiser(organiserId + "").then(res => {
            const org = new Organiser(
                res.id,
                res.name,
                res.description,
                res.street,
                res.number,
                res.appartment,
                res.postalCode,
                res.city
            )
            this.setSelectedOrganiser(org);
            this.setIsLoading(false);
            this.setHadLoadingError(false);
        }).catch(error => {
            this.setIsLoading(false);
            this.setHadLoadingError(true);
        });

    }

    @action
    //TODO: Don't know that it is a good idea to add new organiser here as it becomes dependent on state in the user store.
    editOrganiser(organiser: IOrganiser) {
        this.isSubmitting = true;
        const client = new OrganiserClient();
        this.newOrganiser = organiser

        client.editOrganiser({
            userId: this.rootStore.userStore.currentUser.id,
            organiserId: organiser.id,
            name: organiser.name,
            description: organiser.description,
            street: organiser.street,
            number: organiser.streetNumber,
            appartment: organiser.appartment,
            postalCode: organiser.postalCode,
            city: organiser.city
        }).then(res => {
            this.isSubmitting = false;
            this.submitSuccess = true;
        }).catch(error => {
            this.hadSubmissionError = true;
            this.submitSuccess = false;
            this.isSubmitting = false;
        })
    }

    @action
    setIsLoading(isLoading: boolean) {
        this.isLoading = isLoading
    }

    @action
    setHadLoadingError(hadError: boolean) {
        this.hadLoadingError = hadError;
    }

    @action
    setSelectedOrganiser(organiser: IOrganiser) {
        this.selectedOrganiser = organiser
    }

    @action
    setNewOrganiser(organiser: IOrganiser) {
        this.newOrganiser = organiser;
    }

    @action
    setEditedOrganiser(organiser: IOrganiser) {
        this.editedOrganiser = organiser;
    }

    @action
    resetSubmitState()
    {
        this.submitSuccess = false;
        this.isSubmitting = false;
        this.hadSubmissionError = false;
    }
}

export { OrganiserStore }