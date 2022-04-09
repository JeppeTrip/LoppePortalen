import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { useLocalObservable } from 'mobx-react-lite';

const UserFormUiContext = createContext(null)

export const UserFormUiContextProvider  = ({ children }) => {
    const userFormUiStore = useLocalObservable(() => new UserFormUiStore())

    return(
        <UserFormUiContextProvider.Provider value={userFormUiStore}>{children}</UserFormUiContextProvider.Provider>
    )
}

export const useOrganiserStore = () => useContext(UserFormUiContext);