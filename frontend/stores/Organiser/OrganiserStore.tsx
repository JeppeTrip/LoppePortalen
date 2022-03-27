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
            },
            {
                id : 2,
                name : "Moria Merchandising",
                description : "The most amazing organiser you have ever seen",
                street : "Test Street",
                streetNumber : "123",
                appartment: "st tv",
                postalCode : "1234",
                city : "Test City"
            },
            {
                id : 3,
                name : "Rivendell Beauty Parlor",
                description : "The most amazing organiser you have ever seen",
                street : "Test Street",
                streetNumber : "123",
                appartment: "st tv",
                postalCode : "1234",
                city : "Test City"
            },
            {
                id : 4,
                name : "Mordor IF",
                description : "The most amazing organiser you have ever seen",
                street : "Test Street",
                streetNumber : "123",
                appartment: "st tv",
                postalCode : "1234",
                city : "Test City"
            },
            {
                id : 5,
                name : "Gondor FC",
                description : "The most amazing organiser you have ever seen",
                street : "Test Street",
                streetNumber : "123",
                appartment: "st tv",
                postalCode : "1234",
                city : "Test City"
            },
            {
                id : 6,
                name : "Isengard Squash Team",
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

    @action
    addOrganiser(organiser : IOrganiser)
    {
        //TODO: Fix this so it calls backend and obviously gets id from there.
        organiser.id = Math.floor(Math.random()*1000);
        this.organisers.push(organiser);
        return organiser.id;
    }

}

export { OrganiserStore }