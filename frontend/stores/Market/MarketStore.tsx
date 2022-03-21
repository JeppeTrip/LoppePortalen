import * as React from 'react';
import { MarketContextType, IMarket } from '../../@types/Market';
import { MarketClient } from '../models';

const MarketStore: React.FC<React.ReactNode> = ({ children }) => {
    const [markets, setMarkets] = React.useState<IMarket[]>([]);
    const addMarket = (market: IMarket) => {
        const newMarket = {
            id: null,
            organiserId: market.organiserId,
            name: market.name,
            startDate: market.startDate,
            endDate: market.endDate,
            description: market.description
        };
        var client = new MarketClient();
        client.createMarket(
            {
                organiserId: market.organiserId,
                marketName: market.name,
                startDate: market.startDate,
                endDate: market.endDate,
                description: market.description
            }
        ).then(res => {
            newMarket.id = res.marketId;
            setMarkets([...markets, newMarket]);
        }).catch(error => console.debug(error))
    };
    const getMarketInstance = (id: number): IMarket => {
        var client = new MarketClient();
        var result;
        client.getMarketInstance(id + "").then(
            market => {
                return (
                    {
                        id: market.marketId,
                        organiserId: market.organiserId,
                        name: market.marketName,
                        startDate: market.startDate,
                        endDate: market.endDate,
                        description: market.description
                    }
                )
            });
    }

    return (
        <MarketContext.Provider
            value={{ markets, addMarket, getMarketInstance }}>
            {children}
        </MarketContext.Provider>
    )
}

const MarketContext = React.createContext<MarketContextType | null>(null);
export { MarketContext, MarketStore }