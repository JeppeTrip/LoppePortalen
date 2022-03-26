import { Avatar, ListItem, ListItemAvatar, ListItemButton, ListItemText } from '@mui/material';
import { useRouter } from 'next/router';
import React, { FC, useContext, useEffect, useState } from 'react';
import { IMarket } from '../../@types/Market';
import DateDisplay from '../DateDisplay';
import styles from './styles.module.css';
import ImageIcon from '@mui/icons-material/Image'
import { StoreContext } from '../../stores/StoreContext';

type Props = {
    Market: IMarket
}

const MarketListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    const handleOnClick = (event) => {
        event.preventDefault();
        if (router.isReady) {
            stores.marketStore.setSelectedMarket(props.Market)
            router.push('/market/'+props.Market.id, undefined, { shallow: true });
        }
    }

    return (
        <ListItemButton onClick={handleOnClick}>
            <ListItemAvatar>
                <Avatar>
                    <ImageIcon />
                </Avatar>
            </ListItemAvatar>
            <ListItemText primary={props.Market.name} secondary={props.Market.startDate.toLocaleDateString() + " - " + props.Market.endDate.toLocaleDateString()} />
        </ListItemButton>
    )
}

export default MarketListItem