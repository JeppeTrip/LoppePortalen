import React, { FC, createContext, ReactNode, ReactElement, useEffect } from 'react';
import { RootStore } from './RootStore';

let store: RootStore;

// function to initialize the store
function initializeStore(): RootStore {
    const _store = store ?? new RootStore();

    // For server side rendering always create a new store
    if (typeof window === "undefined") return _store;

    // Create the store once in the client
    if (!store) store = _store;

    return _store;
}


export const StoreContext = createContext<RootStore>({} as RootStore);

export type StoreComponent = FC<{
    children: ReactNode;
}>;

export const StoreProvider: StoreComponent = ({ children }): ReactElement => {
    const store = initializeStore()

    /*
        moved authorization here because the rootstore isn't available in the _app.tsx file
     */
    useEffect(() => {
        store.authStore.auth.initialize();
    }, []);

    return (
        <StoreContext.Provider value={store}>{children}</StoreContext.Provider>
    )
}

