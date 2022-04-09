import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { useLocalObservable } from 'mobx-react-lite';

const MarketProfileUiContext = createContext(null)

export const MarketProfileUiContextProvider  = ({ children }) => {
    const marketProfileUiStore = useLocalObservable(() => new MarketProfileUiStore())

    return(
        <MarketProfileUiContextProvider.Provider value={marketProfileUiStore}>{children}</MarketProfileUiContextProvider.Provider>
    )
}

export const useMarketProfileUiContext = () => useContext(MarketProfileUiContext);