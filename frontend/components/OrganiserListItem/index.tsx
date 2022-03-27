import { Avatar, ListItem, ListItemAvatar, ListItemButton, ListItemText } from '@mui/material';
import { useRouter } from 'next/router';
import React, { FC, useContext, useEffect, useState } from 'react';
import { IMarket } from '../../@types/Market';
import DateDisplay from '../DateDisplay';
import styles from './styles.module.css';
import ImageIcon from '@mui/icons-material/Image'
import { StoreContext } from '../../stores/StoreContext';
import { IOrganiser } from '../../@types/Organiser';
import CorporateFareIcon from '@mui/icons-material/CorporateFare';

type Props = {
    Organiser: IOrganiser
}

const OrganiserListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    const handleOnClick = (event) => {
        event.preventDefault();
        //TODO: Make this redirect to an organiser profile.
        /*
        if (router.isReady) {
            stores.marketStore.setSelectedMarket(props.Market)
            router.push('/market/'+props.Market.id, undefined, { shallow: true });
        }
        */
    }

    return (
        <ListItemButton onClick={handleOnClick}>
            <ListItemAvatar>
                <Avatar>
                    <CorporateFareIcon />
                </Avatar>
            </ListItemAvatar>
            <ListItemText primary={props.Organiser.name}/>
        </ListItemButton>
    )
}

export default OrganiserListItem