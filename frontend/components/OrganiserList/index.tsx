import React, {FC, useContext, useEffect, useState} from 'react';
import { OrganiserContext } from '../../stores/Organiser/OrganiserStore';
import ListItem from './ListItem/ListItem';
import styles from './styles.module.css';

type Props = {

}

const OrganiserList: FC<Props> = (props: Props) => {
    const store = useContext(OrganiserContext);
    const [organisers, setOrganisers] = useState([]);

    useEffect(() => {
        store.getAllOrganisers()
            .then(res => setOrganisers(res));
    },[])

    return(
        <div className={styles.ol}>
            <ul>
                {
                    organisers.map(o => 
                        <ListItem id={o.id} name={o.name}/>
                    )
                }
            </ul>
        </div>
    )
}

export default OrganiserList;