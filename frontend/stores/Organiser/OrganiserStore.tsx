import {makeObservable, observable} from 'mobx'
import {useLocalObservable} from 'mobx-react-lite'
import { CreateOrganiserRequest } from '../models';
import OrganiserContext from './OrganiserContext'

class Organiser {
    id?: number;
    name: string;
    description: string|null;
    street?: string | null;
    number?: string | null;
    appartment?: string | null;
    postalCode?: string | null;
    city?: string | null;

    constructor(){
        makeObservable(this, {
            id: observable,
            name: observable,
            description: observable,
            street: observable,
            number: observable,
            appartment: observable,
            postalCode: observable,
            city: observable
        })
    }
}

const OrganiserStore = ({ children }) => {
    const organiserStore = useLocalObservable(() => ({
        Organiser: null,
        
        CreateOrganiser: (organiser : CreateOrganiserRequest) => {
            console.log("create this organiser or whatever.")
        }
    }));

    return(
        <OrganiserContext.Provider value={organiserStore}>{ children }</OrganiserContext.Provider>
    );
}

export default OrganiserStore;