import * as React from 'react';
import { MarketContextType, IMarket } from '../../@types/Market';

const MarketStore: React.FC<React.ReactNode> = ({ children }) => {
    const [markets, setMarkets] = React.useState<IMarket[]>([]);
    const addMarket = (market: IMarket) => {
        const newMarket = {
            id : null,
            name : market.name,
            startDate : market.startDate,
            endDate : market.endDate,
            description : market.description
        };
        setMarkets([...markets, newMarket]);
    };

    return (
        <MarketContext.Provider
            value={{ markets, addMarket }}>
            {children}
        </MarketContext.Provider>
    )
}

const MarketContext = React.createContext<MarketContextType | null>(null);
export {MarketContext, MarketStore}