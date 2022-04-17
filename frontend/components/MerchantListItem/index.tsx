import StoreIcon from '@mui/icons-material/Store';
import { ListItem, ListItemAvatar, ListItemText } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Merchant } from '../../NewStores/@DomainObjects/Merchant';
type Props = {
    merchant: Merchant
}



const MerchantListItem: FC<Props> = (props: Props) => {
    return (
        <ListItem>
            <ListItemAvatar>
                <StoreIcon />
            </ListItemAvatar>
            <ListItemText
                primary={props.merchant.name}
            />

        </ListItem >
    );
}


export default observer(MerchantListItem);