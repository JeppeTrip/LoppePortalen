import {makeObservable, observable} from 'mobx'
import {useLocalObservable} from 'mobx-react-lite'
import React, { useContext, useEffect, useState } from 'react';
import { ClientContext } from '../../services/Clients';
import { CreateOrganiserRequest } from '../models';

const OrganiserStore = ({ children }) => {
    const clients = useContext(ClientContext);
    const organiserStore = useLocalObservable(() => ({
        addOrganiser: async organiser => {
            return clients.organiserClient.createOrganiser({
                name: organiser.name,
                street: organiser.street,
                number: organiser.streetNumber,
                appartment: organiser.appartment,
                postalCode: organiser.postalcode,
                city: organiser.city
            });
        },
        getAllOrganisers: async () => {
            return clients.organiserClient.getAllOrganisers();
        },

        getOrganisers: async (pageNumber, pageSize) => {
            return clients.organiserClient.getOrganisers(pageNumber, pageSize);
        }
    }));

    return(
        <OrganiserContext.Provider value={organiserStore}>{ children }</OrganiserContext.Provider>
    );
}

const OrganiserContext = React.createContext();

export  {OrganiserContext, OrganiserStore};