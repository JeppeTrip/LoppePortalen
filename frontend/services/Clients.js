import { useLocalObservable } from "mobx-react-lite";
import React, { useEffect } from "react"
import { MarketClient, OrganiserClient } from "../stores/models";

const ClientContext = React.createContext()

const ClientStore = ({ children }) => {
    const clientStore = useLocalObservable(() => ({
        organiserClient: null,
        marketClient: null
    }));

    useEffect(() => {
        clientStore.organiserClient = new OrganiserClient();
        clientStore.marketClient = new MarketClient();

    }, []);

    return(
        <ClientContext.Provider value={clientStore}>{ children }</ClientContext.Provider>
    );
}

export {ClientContext, ClientStore};