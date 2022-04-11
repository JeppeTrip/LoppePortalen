import { Avatar, ButtonGroup, Grid, IconButton, ListItem, ListItemAvatar, ListItemButton, ListItemText } from '@mui/material';
import { useRouter } from 'next/router';
import React, { FC, useContext, useEffect, useState } from 'react';
import { Market } from '../../@types/Market';
import DateDisplay from '../DateDisplay';
import styles from './styles.module.css';
import ImageIcon from '@mui/icons-material/Image'
import { StoreContext } from '../../stores/StoreContext';
import EditIcon from '@mui/icons-material/Edit';
import CancelIcon from '@mui/icons-material/Cancel';
import { observer } from 'mobx-react-lite';

type Props = {
    Market: Market,
    showControls?: boolean
}

const MarketListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    const handleOnClick = (event) => {
        event.preventDefault();
        if (router.isReady) {
            router.push('/market/' + props.Market.id, undefined, { shallow: true });
        }
    }

    const handleClickEdit = (event) => {
        event.preventDefault();
        if (router.isReady) {
            router.push('/market/edit/' + props.Market.id, undefined, { shallow: true });
        }
    }

    const handleClickCancel = (event) => {
        stores.marketStore.cancelMarket(props.Market);
    }

    return (
        <ListItem
            secondaryAction={
                props.showControls &&
                <Grid container spacing={2}>
                    <Grid item>
                        <IconButton edge="end"
                            onClick={handleClickEdit}>
                            <EditIcon />
                        </IconButton>
                    </Grid>
                    <Grid item>
                        <IconButton edge="end"
                            onClick={handleClickCancel}
                            disabled={props.Market.isCancelled}>
                            <CancelIcon />
                        </IconButton>
                    </Grid>

                </Grid>
            }
            disablePadding>
            <ListItemButton onClick={handleOnClick}>
                <ListItemAvatar>
                    <Avatar>
                        <ImageIcon />
                    </Avatar>
                </ListItemAvatar>
                <ListItemText
                    primary={props.Market.name}
                    secondary={props.Market.startDate.toLocaleDateString() + " - " + props.Market.endDate.toLocaleDateString()} />
            </ListItemButton>
        </ListItem>

    )
}

export default observer(MarketListItem);