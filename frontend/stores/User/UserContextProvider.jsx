import React, {createContext, useContext} from 'react';
import { useLocalObservable } from 'mobx-react-lite';

const UserContext = createContext(null)

export const UserStoreContextProvider  = ({ children }) => {
    const UserStore = useLocalObservable(() => new UserStore())

    return(
        <UserStoreContextProvider.Provider value={UserStore}>{children}</UserStoreContextProvider.Provider>
    )
}

export const useUiStateContext = () => useContext(UserContext);