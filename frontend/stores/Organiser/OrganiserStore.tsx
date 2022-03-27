import { action, makeAutoObservable } from 'mobx';
import * as React from 'react';
import { MarketContextType, IMarket } from '../../@types/Market';
import { IOrganiser } from '../../@types/Organiser';
import { MarketClient, OrganiserClient } from '../models';
import { RootStore } from '../RootStore';

class OrganiserStore {
    rootStore: RootStore;
    organisers: IOrganiser[] = [];

    isLoading = true;
    hadLoadingError = false;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
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
        //TODO: Fix this so it calls backend and obviously gets id from there.
        organiser.id = Math.floor(Math.random() * 1000);
        this.organisers.push(organiser);
        return organiser.id;
    }

}

export { OrganiserStore }