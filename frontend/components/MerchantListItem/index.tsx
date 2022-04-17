import StoreIcon from '@mui/icons-material/Store';
import { Grid, IconButton, ListItem, ListItemAvatar, ListItemButton, ListItemText } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRouter } from 'next/router';
import { FC, useCallback } from "react";
import { Merchant } from '../../NewStores/@DomainObjects/Merchant';
import EditIcon from '@mui/icons-material/Edit';

type Props = {
    merchant: Merchant
    showControls?: boolean
}

const MerchantListItem: FC<Props> = (props: Props) => {
    const router = useRouter()

    const redirect = useCallback(() => {
        if (router.isReady)
            router.push(`merchant/${props.merchant.id}`, undefined, { shallow: true })
    }, [router.isReady])

    const redirectEdit = useCallback(() => {
        if (router.isReady)
            router.push(`merchant/edit/${props.merchant.id}`, undefined, { shallow: true })
    }, [router.isReady])

    return (
        <ListItem
            disablePadding
            secondaryAction={
                props.showControls &&
                <Grid container spacing={2}>
                    <Grid item>
                        <IconButton edge="end"
                            onClick={() => redirectEdit()}>
                            <EditIcon />
                        </IconButton>
                    </Grid>
                </Grid>
            }>
            <ListItemButton onClick={() => redirect()}>
                <ListItemAvatar>
                    <StoreIcon />
                </ListItemAvatar>
                <ListItemText
                    primary={props.merchant.name}
                />
            </ListItemButton>


        </ListItem >
    );
}


export default observer(MerchantListItem);