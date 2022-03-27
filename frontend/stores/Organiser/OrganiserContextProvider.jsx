import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { MarketStore } from './MarketStore';
import { useLocalObservable } from 'mobx-react-lite';

const OrganiserContext = createContext(null)

export const OrganiserContextProvider  = ({ children }) => {
    const organiserStore = useLocalObservable(() => new OrganiserStore())

    return(
        <OrganiserContextProvider.Provider value={organiserStore}>{children}</OrganiserContextProvider.Provider>
    )
}

export const useOrganiserStore = () => useContext(OrganiserContext);