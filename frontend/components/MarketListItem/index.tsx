import { Avatar, ButtonGroup, Grid, IconButton, ListItem, ListItemAvatar, ListItemButton, ListItemText } from '@mui/material';
import { useRouter } from 'next/router';
import React, { FC, useContext, useEffect, useState } from 'react';
import { IMarket } from '../../@types/Market';
import DateDisplay from '../DateDisplay';
import styles from './styles.module.css';
import ImageIcon from '@mui/icons-material/Image'
import { StoreContext } from '../../stores/StoreContext';
import EditIcon from '@mui/icons-material/Edit';
import CancelIcon from '@mui/icons-material/Cancel';

type Props = {
    Market: IMarket,
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
        console.log("navigate to edit page.")
        event.preventDefault();
        if (router.isReady) {

        }
    }

    const handleClickCancel = (event) => {
        console.log("cancel market")
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
                            onClick={handleClickCancel}>
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

export default MarketListItem