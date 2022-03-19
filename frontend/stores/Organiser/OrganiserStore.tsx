import * as React from 'react';
import { OrganiserContextType, IOrganiser } from '../../@types/Organiser';
import { OrganiserClient } from "../../stores/models";

const OrganiserStore: React.FC<React.ReactNode> = ({ children }) => {
    const [organisers, setOrganisers] = React.useState<IOrganiser[]>([]);
    const addOrganiser = (organiser: IOrganiser) => {
        const newOrganiser = {
            id: null,
            name: organiser.name,
            description: organiser.description,
            street: organiser.street,
            streetNumber: organiser.streetNumber,
            appartment: organiser.appartment,
            postalCode: organiser.postalCode,
            city: organiser.city
        };
        var client = new OrganiserClient();
        client.createOrganiser({
            name: newOrganiser.name,
            description: newOrganiser.description,
            street: newOrganiser.street,
            number: newOrganiser.streetNumber,
            appartment: newOrganiser.appartment,
            postalCode: newOrganiser.postalCode,
            city: newOrganiser.city
        }).then((res) => {
            //TODO: is this the right thing todo?
            newOrganiser.id = res.id;
            setOrganisers([...organisers, newOrganiser]);
        }).catch((error) => {
            //TODO håndter error.
            console.error("Something went wrong when uploading organiser.")
        })
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