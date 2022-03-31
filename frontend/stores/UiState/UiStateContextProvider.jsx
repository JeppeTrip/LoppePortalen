import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { MarketStore } from './MarketStore';
import { useLocalObservable } from 'mobx-react-lite';

const UiStateContext = createContext(null)

export const UiStateContextProvider  = ({ children }) => {
    const UiStateStore = useLocalObservable(() => new UiStateStore())

    return(
        <UiStateContextProvider.Provider value={UiStateStore}>{children}</UiStateContextProvider.Provider>
    )
}

export const useUiStateContext = () => useContext(UiStateContext);