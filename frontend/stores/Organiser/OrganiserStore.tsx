import { action, makeAutoObservable } from 'mobx';
import * as React from 'react';
import { MarketContextType, IMarket } from '../../@types/Market';
import { IOrganiser } from '../../@types/Organiser';
import { MarketClient, OrganiserClient } from '../models';
import { RootStore } from '../RootStore';

class OrganiserStore {
    rootStore: RootStore;
    organisers: IOrganiser[] = [];
    newOrganiser: IOrganiser;

    isLoading = true;
    hadLoadingError = false;

    isSubmitting = false;
    hadSubmissionError = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.newOrganiser = {            
            id: null,
            name: "",
            description: "",
            street: "",
            streetNumber: "",
            appartment: "",
            postalCode: "",
            city: ""}
        this.rootStore = rootStore;
    }

    loadOrganisers() {
        this.hadLoadingError = false;
        this.isLoading = true;
        const client = new OrganiserClient();
        client.getAllOrganisers().then(res => {
            var result = res.map<IOrganiser>(org => {
                return (
                    {
                        //TODO: Fix this so the backend call actually returns all the organiser information, can do this when I get around to needing to show the organiser profile.
                        id: org.id,
                        name: org.name,
                        description: "",
                        street: "",
                        streetNumber: "",
                        appartment: "",
                        postalCode: "",
                        city: ""
                    }
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
    addOrganiser(organiser: IOrganiser) {
        this.isSubmitting = true;
        const client = new OrganiserClient();
        this.newOrganiser = organiser

        client.createOrganiser({
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
        }).catch(error => {
            this.hadSubmissionError = true;
            this.isSubmitting = false;
            this.newOrganiser.id = -1;
        })
    }
}

export { OrganiserStore }