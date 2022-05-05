import ImageIcon from '@mui/icons-material/Image';
import { Avatar, IconButton, ListItem, ListItemAvatar, ListItemText } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";
import { ModelState } from '../../@types/ModelState';
import { ContactInfo } from '../../NewStores/@DomainObjects/ContactInfo';
import { StoreContext } from "../../NewStores/StoreContext";
import CancelIcon from '@mui/icons-material/Cancel';

type Props = {
    contactInfo: ContactInfo
    editing?: boolean
}



const ContactInfoListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    const handleOnDelete = (event) => {
        props.contactInfo.delete()
    }

    return (
        <ListItem secondaryAction={props.editing &&
            <IconButton edge="end"
                onClick={handleOnDelete}>
                <CancelIcon />
            </IconButton>}>
            <ListItemAvatar>
                <Avatar>
                    <ImageIcon />
                </Avatar>
            </ListItemAvatar>
            <ListItemText primary={props.contactInfo.value} secondary={props.contactInfo.state === ModelState.ERROR && "Something went wrong trying to remove this item."}/>
        </ListItem>
    );
}


export default observer(ContactInfoListItem);