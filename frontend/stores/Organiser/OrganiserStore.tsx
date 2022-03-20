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

    //TODO maybe make the organiser object a composite object.
    const fetchAllOrganisers = () => {
        var client = new OrganiserClient();
        client.getAllOrganisers()
            .then((res) => {
                var organisers = res.map((org) => {
                    return {
                        id = org.id,
                        name: org.name,
                        description: "",
                        street: "",
                        streetNumber: "",
                        appartment: "",
                        postalCode: "",
                        city: ""
                    } as IOrganiser
                });
                setOrganisers(organisers);
            }
            );
    };
};

return (
    <OrganiserContext.Provider
        value={{ organisers, addOrganiser, fetchAllOrganisers }}>
        {children}
    </OrganiserContext.Provider>
)
}

const OrganiserContext = React.createContext<OrganiserContextType | null>(null);
export { OrganiserContext, OrganiserStore }