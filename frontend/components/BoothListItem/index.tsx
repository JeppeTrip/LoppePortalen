import { Avatar, Chip, Grid, IconButton, ListItem, ListItemAvatar, ListItemButton, ListItemText, Stack, Typography } from "@mui/material";
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
            router.push(`/booth/${props.booth.id}`, undefined, { shallow: true })
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
                <Stack>
                    <Stack direction={"row"}>
                        <ListItemAvatar>
                            <Avatar>
                                <StorefrontIcon />
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemText
                            primary={props.booth.name}
                            secondary={props.booth.stall.market.name + " - " + props.booth.stall.type.name} />
                    </Stack>

                    <Stack spacing={1} direction={"row"}>
                        {
                            props.booth.itemCategories.map(x => <Chip size="small" label={x} />)
                        }
                    </Stack>
                </Stack>

            </ListItemButton>

        </ListItem>
    );
}


export default observer(BoothListItem);