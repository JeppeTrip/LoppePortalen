import { useLocalObservable } from "mobx-react-lite";
import React, { useEffect } from "react"
import { OrganiserClient } from "../stores/models";

const ClientContext = React.createContext()

const ClientStore = ({ children }) => {
    const clientStore = useLocalObservable(() => ({
        organiserClient: null,
    }));

    useEffect(() => {
        clientStore.organiserClient = new OrganiserClient();
    });

    return(
        <ClientContext.Provider value={clientStore}>{ children }</ClientContext.Provider>
    );
}

export {ClientContext, ClientStore};