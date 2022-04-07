import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { useLocalObservable } from 'mobx-react-lite';

const MarketFormUiContext = createContext(null)

export const MarketFormUicontextProvider  = ({ children }) => {
    const marketFormUiStore = useLocalObservable(() => new MarketFormUiStore())

    return(
        <MarketFormUicontextProvider.Provider value={marketFormUiStore}>{children}</MarketFormUicontextProvider.Provider>
    )
}

export const useOrganiserStore = () => useContext(MarketFormUiContext);