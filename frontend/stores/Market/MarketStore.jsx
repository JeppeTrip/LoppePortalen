import {makeObservable, observable} from 'mobx'
import {useLocalObservable} from 'mobx-react-lite'
import React, { useContext, useEffect, useState } from 'react';
import { ClientContext } from '../../services/Clients';
import { CreateOrganiserRequest } from '../models';

const MarketStore = ({ children }) => {
    const clients = useContext(ClientContext);

    const marketStore = useLocalObservable(() => ({
        addMarket: async market => {
            console.log(market)
            return clients.marketClient.createMarket({
                organiserId: market.organiserId,
                marketName: market.marketName,
                description: market.description,
                startDate: market.startDate,
                endDate: market.endDate
            });
        }
    }));

    return(
        <MarketContext.Provider value={marketStore}>{ children }</MarketContext.Provider>
    );
}

const MarketContext = React.createContext();

export  {MarketContext, MarketStore};