import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { MarketStore } from './MarketStore';
import { useLocalObservable } from 'mobx-react-lite';

const MarketContext = createContext(null)

export const MarketContextProvider  = ({ children }) => {
    const marketStore = useLocalObservable(() => new MarketStore())

    return(
        <MarketContextProvider.Provider value={marketStore}>{children}</MarketContextProvider.Provider>
    )
}

export const useMarketStore = () => useContext(MarketContext);