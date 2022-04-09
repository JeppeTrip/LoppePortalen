import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { MarketStore } from './MarketStore';
import { useLocalObservable } from 'mobx-react-lite';

const AuthContext = createContext(null)

export const AuthContextProvider  = ({ children }) => {
    const authStore = useLocalObservable(() => new AuthStore())

    return(
        <AuthContextProvider.Provider value={authStore}>{children}</AuthContextProvider.Provider>
    )
}

export const useMarketStore = () => useContext(AuthContext);