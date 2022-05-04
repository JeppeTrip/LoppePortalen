import ImageIcon from '@mui/icons-material/Image';
import { Avatar, ListItem, ListItemAvatar, ListItemText } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";
import { ModelState } from '../../@types/ModelState';
import { ContactInfo } from '../../NewStores/@DomainObjects/ContactInfo';
import { StoreContext } from "../../NewStores/StoreContext";


type Props = {
    contactInfo: ContactInfo
}



const ContactInfoListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    const handleOnAdd = (event) => {
        props.contactInfo.addToOrganiser()
    }

    /** This is a little bit hacky but component is rendered in parent when model state is new,
     * and since it only exists in the parent component state, we can just have the parent component
     * react to when the modelstate isn't new, and then remove the input list component from the list.
     */
    const handleOnDelete = (event) => {
        props.contactInfo.state = ModelState.IDLE
    }

    return (
        <ListItem>
            <ListItemAvatar>
                <Avatar>
                    <ImageIcon />
                </Avatar>
            </ListItemAvatar>
            <ListItemText primary={props.contactInfo.value} />
        </ListItem>
    );
}


export default observer(ContactInfoListItem);