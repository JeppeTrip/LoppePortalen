import { Avatar, Grid, IconButton, ListItem, ListItemAvatar, ListItemButton, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useCallback } from "react";
import { Booth } from "../../NewStores/@DomainObjects/Booth";
import StorefrontIcon from '@mui/icons-material/Storefront';
import { useRouter } from "next/router";
import EditIcon from '@mui/icons-material/Edit';

type Props = {
    booth: Booth
    showEdit?: boolean
}



const BoothListItem: FC<Props> = (props: Props) => {
    const router = useRouter()

    const redirect = useCallback(() => {
        if (router.isReady)
            router.push(`booth/${props.booth.id}`, undefined, { shallow: true })
    }, [router, router.isReady])

    const handleClickEdit = useCallback((event) => {
        event.preventDefault();
        if (router.isReady) {
            router.push('/booth/edit/' + props.booth.id, undefined, { shallow: true });
        }
    }, [router, router.isReady])

    return (
        <ListItem
            secondaryAction={
                props.showEdit &&
                <IconButton edge="end"
                    onClick={handleClickEdit}>
                    <EditIcon />
                </IconButton>}
            disablePadding
        >
            <ListItemButton onClick={redirect}>
                <ListItemAvatar>
                    <Avatar>
                        <StorefrontIcon />
                    </Avatar>
                </ListItemAvatar>
                <Grid container spacing={2} alignItems="center">
                    <Grid item xs={8}>
                        <Stack>
                            <Typography
                                variant="h6">
                                {
                                    props.booth.name
                                }
                            </Typography>
                            <Grid container spacing={2}>
                                <Grid item>
                                    <Typography variant="caption">
                                        {
                                            props.booth.stall.market.name
                                        }
                                    </Typography>
                                </Grid>
                                <Grid item>
                                    <Typography variant="caption">
                                        {
                                            props.booth.stall.type.name
                                        }
                                    </Typography>
                                </Grid>
                            </Grid>

                        </Stack>
                    </Grid>
                </Grid>
            </ListItemButton>

        </ListItem>
    );
}


export default observer(BoothListItem);