import React, {createContext, ReactElement, ReactNode, useContext} from 'react';
import { useLocalObservable } from 'mobx-react-lite';

const StallFormUiContext = createContext(null)

export const StallFormUiContextProvider  = ({ children }) => {
    const StallFormUiStore = useLocalObservable(() => new StallFormUiStore())

    return(
        <StallFormUiContextProvider.Provider value={StallFormUiStore}>{children}</StallFormUiContextProvider.Provider>
    )
}

export const useStallFormUiContext = () => useContext(StallFormUiContext);