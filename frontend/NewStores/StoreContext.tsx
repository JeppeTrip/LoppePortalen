import React, { FC, createContext, ReactNode, ReactElement, useEffect } from 'react';
import { RootStore } from './RootStore';

export const StoreContext = createContext<RootStore>({} as RootStore);

export type StoreComponent = FC<{
    children: ReactNode;
}>;

export const StoreProvider: StoreComponent = ({ children }): ReactElement => {
    const rootStore = new RootStore();

    useEffect(() => {
        console.log("on first mounts")
        console.log(rootStore)
        console.log(rootStore.authStore.auth)
        rootStore.authStore.auth.initialize();
    }, []);

return (
    <StoreContext.Provider value={rootStore}>{children}</StoreContext.Provider>
)
}