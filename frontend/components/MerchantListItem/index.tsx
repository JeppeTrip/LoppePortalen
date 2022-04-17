import StoreIcon from '@mui/icons-material/Store';
import { ListItem, ListItemAvatar, ListItemButton, ListItemText } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRouter } from 'next/router';
import { FC, useCallback } from "react";
import { Merchant } from '../../NewStores/@DomainObjects/Merchant';
type Props = {
    merchant: Merchant
}

const MerchantListItem: FC<Props> = (props: Props) => {
    const router = useRouter()

    const redirect = useCallback(() => {
        if (router.isReady)
            router.push(`merchant/${props.merchant.id}`, undefined, { shallow: true })
    }, [router.isReady])

    return (
        <ListItem>
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