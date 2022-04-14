import { Avatar, Grid, IconButton, ListItem, ListItemAvatar, ListItemButton, ListItemText } from '@mui/material';
import { useRouter } from 'next/router';
import React, { FC} from 'react';
import ImageIcon from '@mui/icons-material/Image'
import EditIcon from '@mui/icons-material/Edit';
import CancelIcon from '@mui/icons-material/Cancel';
import { observer } from 'mobx-react-lite';
import { Market } from '../../NewStores/@DomainObjects/Market';

type Props = {
    Market: Market,
    showControls?: boolean
}

const MarketListItem: FC<Props> = (props: Props) => {
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
        console.log("NOT IMPLEMENTED YET")
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