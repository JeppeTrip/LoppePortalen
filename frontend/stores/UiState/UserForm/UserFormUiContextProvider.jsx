import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { useLocalObservable } from 'mobx-react-lite';

const UserFormUiContext = createContext(null)

export const UserFormUiContextProvider  = ({ children }) => {
    const userFormUiStore = useLocalObservable(() => new MarketFormUiStore())

    return(
        <UserFormUiContextProvider.Provider value={marketFormUiStore}>{children}</UserFormUiContextProvider.Provider>
    )
}

export const useOrganiserStore = () => useContext(UserFormUiContext);