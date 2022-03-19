import * as React from 'react';
import { OrganiserContextType, IOrganiser } from '../../@types/Organiser';

const OrganiserStore: React.FC<React.ReactNode> = ({ children }) => {
    const [organisers, setOrganisers] = React.useState<IOrganiser[]>([]);
    const addOrganiser = (organiser: IOrganiser) => {
        const newOrganiser = {
            id: Math.floor(Math.random() * 100) + 1,
            name: organiser.name,
            description: organiser.description,
            street: organiser.street,
            streetNumber: organiser.streetNumber,
            appartment: organiser.appartment,
            postalCode: organiser.postalCode,
            city: organiser.city
        };
        setOrganisers([...organisers, newOrganiser]);
    };

    return (
        <OrganiserContext.Provider
            value={{organisers, addOrganiser }}>
            {children}
        </OrganiserContext.Provider>
    )
}

const OrganiserContext = React.createContext<OrganiserContextType | null>(null);
export {OrganiserContext, OrganiserStore}