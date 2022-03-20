import React, { FC, useContext, useEffect, useState } from 'react';
import { OrganiserContext } from '../../stores/Organiser/OrganiserStore';
import ListItem from './ListItem/ListItem';
import styles from './styles.module.css';

type Props = {

}

const OrganiserList: FC<Props> = (props: Props) => {
    const store = useContext(OrganiserContext);

    useEffect(() => {
        store.fetchAllOrganisers();
    }, [])

    return (
        <div  className={styles.ol} >
            <ul id={'organiserList'}>
                {
                    store.organisers.map(o =>
                        <ListItem id={o.id} key={o.id} name={o.name} />
                    )
                }
            </ul>
        </div>
    )
}

export default OrganiserList;