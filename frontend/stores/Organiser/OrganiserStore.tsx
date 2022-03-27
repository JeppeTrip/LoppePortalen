import { action, makeAutoObservable } from 'mobx';
import * as React from 'react';
import { MarketContextType, IMarket } from '../../@types/Market';
import { IOrganiser } from '../../@types/Organiser';
import { MarketClient } from '../models';
import { RootStore } from '../RootStore';

class OrganiserStore {
    rootStore: RootStore;
    organisers: IOrganiser[] = [];

    isLoading = true;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        //Don't actually think this is a good idea. Since all data will be loaded at once.
        this.loadOrganisers();
    }

    loadOrganisers() {
        this.isLoading = true;
        //TODO: Replace with a fetch, just have placeholder data for now.
        this.organisers = [
            {
                id : 1,
                name : "The Best Organiser",
                description : "The most amazing organiser you have ever seen",
                street : "Test Street",
                streetNumber : "123",
                appartment: "st tv",
                postalCode : "1234",
                city : "Test City"
            }
        ]
        this.isLoading = false;

    }

}

export { OrganiserStore }