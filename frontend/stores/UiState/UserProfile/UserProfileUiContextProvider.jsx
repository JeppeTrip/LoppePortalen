import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { useLocalObservable } from 'mobx-react-lite';

const UserProfileUiContext = createContext(null)

export const UserProfileUiContextProvider  = ({ children }) => {
    const userProfileUiStore = useLocalObservable(() => new MarketFormUiStore())

    return(
        <UserProfileUiContextProvider.Provider value={marketFormUiStore}>{children}</UserProfileUiContextProvider.Provider>
    )
}

export const useOrganiserStore = () => useContext(UserProfileUiContext);