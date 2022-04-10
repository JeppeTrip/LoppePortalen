import { Avatar, IconButton, ListItem, ListItemAvatar, ListItemButton, ListItemText } from '@mui/material';
import { useRouter } from 'next/router';
import React, { FC, useContext, useEffect, useState } from 'react';
import { IMarket } from '../../@types/Market';
import DateDisplay from '../DateDisplay';
import styles from './styles.module.css';
import ImageIcon from '@mui/icons-material/Image'
import { StoreContext } from '../../stores/StoreContext';
import { IOrganiser } from '../../@types/Organiser';
import CorporateFareIcon from '@mui/icons-material/CorporateFare';
import EditIcon from '@mui/icons-material/Edit';

type Props = {
    Organiser: IOrganiser
    showEdit?: boolean
}

const OrganiserListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    const handleOnClick = (event) => {
        event.preventDefault();
        if (router.isReady) {
            router.push('/organiser/show/' + props.Organiser.id, undefined, { shallow: true });
        }
    }

    const handleClickEdit = (event) => {
        event.preventDefault();
        if (router.isReady) {
            router.push('/organiser/edit/' + props.Organiser.id, undefined, { shallow: true });
        }
    }

    return (
        <ListItem
            secondaryAction={
                props.showEdit &&
                <IconButton edge="end"
                    onClick={handleClickEdit}>
                    <EditIcon />
                </IconButton>}
            disablePadding>
            <ListItemButton onClick={handleOnClick}>
                <ListItemAvatar>
                    <Avatar>
                        <CorporateFareIcon />
                    </Avatar>
                </ListItemAvatar>
                <ListItemText primary={props.Organiser.name} />
            </ListItemButton>
        </ListItem >

    )
}

export default OrganiserListItem