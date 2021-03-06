import CancelIcon from '@mui/icons-material/Cancel';
import EditIcon from '@mui/icons-material/Edit';
import ImageIcon from '@mui/icons-material/Image';
import { Avatar, Chip, Grid, IconButton, ListItem, ListItemAvatar, ListItemButton, ListItemText, Stack, Tooltip, Typography } from '@mui/material';
import { observer } from 'mobx-react-lite';
import { useRouter } from 'next/router';
import React, { FC } from 'react';
import { ModelState } from '../../@types/ModelState';
import { Market } from '../../NewStores/@DomainObjects/Market';
import BookOnlineIcon from '@mui/icons-material/BookOnline';

type Props = {
    Market: Market,
    editing?: boolean
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
            props.Market.state = ModelState.EDITING
            router.push('/market/edit/' + props.Market.id, undefined, { shallow: true });
        }
    }

    const handleClickCancel = (event) => {
        console.log("NOT IMPLEMENTED YET")
    }

    const handleClickBook = (event) => {
        if (router.isReady)
            router.push(`market/${props.Market.id}/bookstall`, undefined, { shallow: true });
    }

    return (
        <ListItem
            disablePadding
            secondaryAction={
                props.editing ?
                    (
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
                    )
                    :
                    (
                        <Tooltip title={"Book Stall"}>
                            <IconButton edge="end"
                                disabled={props.Market.availableStallCount === 0}
                                onClick={handleClickBook}>
                                <BookOnlineIcon />
                            </IconButton>
                        </Tooltip>

                    )
            }>
            <ListItemButton
                onClick={handleOnClick} >
                    <Stack>
                        <Stack direction={"row"} alignItems={"center"}>
                            <ListItemAvatar>
                                <Avatar>
                                    <ImageIcon />
                                </Avatar>
                            </ListItemAvatar>
                            <ListItemText
                                primary={props.Market.name}
                                secondary={props.Market.startDate.toLocaleDateString() + " - " + props.Market.endDate.toLocaleDateString()} />
                        </Stack>
                        <Stack spacing={1} direction={"row"}>
                            {
                                props.Market.itemCategories.map(x => <Chip key={`category_${props.Market.id}_${x}`} size="small" label={x} />)
                            }
                        </Stack>
                        <Grid container spacing={1}>
                            <Grid item>
                                <Typography variant={"caption"}>
                                    {`#Stalls: ${props.Market.totalStallCount}`}
                                </Typography>
                            </Grid>
                            <Grid item>
                                <Typography variant={"caption"}>
                                    {`#Available Stalls: ${props.Market.availableStallCount}`}
                                </Typography>
                            </Grid>
                            <Grid item>
                                <Typography variant={"caption"}>
                                    {`#Occupied Stalls: ${props.Market.occupiedStallCount}`}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Stack>
            </ListItemButton>
        </ListItem >
    )
}

export default observer(MarketListItem);