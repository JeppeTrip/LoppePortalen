import AddIcon from '@mui/icons-material/Add';
import { Button, Grid, List } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useCallback, useEffect, useState } from "react";
import { ContactInfo } from '../../NewStores/@DomainObjects/ContactInfo';
import { Organiser } from "../../NewStores/@DomainObjects/Organiser";
import ContactInfoInputListItem from "../../components/ContactInfoNewListItem";
import { ModelState } from '../../@types/ModelState';
import ContactInfoListItem from '../ContactInfoListItem';
import { Merchant } from '../../NewStores/@DomainObjects/Merchant';


type Props = {
    merchant: Merchant;
}

const OrganiserContactsForm: FC<Props> = (props: Props) => {
    const [newContact, setNewContact] = useState<ContactInfo>(null)

    useEffect(() => {
        if(newContact != null && newContact.state === ModelState.IDLE)
            setNewContact(null)
    }, [newContact?.state])

    const handleOnAdd = useCallback(() => {
        props.merchant.addContactInfo(newContact);
    }, [props.merchant, newContact])

    const handleOnDelete = useCallback((contact) => {
        console.log("implement")
    }, [props.merchant])

    return (
        <Grid>
            <Grid item>
                <Button disabled={newContact != null} size="small" startIcon={<AddIcon />} onClick={() => {
                    setNewContact(new ContactInfo())
                }}>
                    Add Contact Information
                </Button>
            </Grid>
            <Grid item>
                <List>
                    {
                        newContact != null && <ContactInfoInputListItem contactInfo={newContact} onAdd={handleOnAdd}/>
                    }
                    
                </List>
                {
                    props.merchant.contactInfo.length === 0 ?
                        (
                            "no contact info found"
                        )
                        :
                        (
                            props.merchant.contactInfo.map(x => <ContactInfoListItem key={`${props.merchant.id}_${x.value}`} contactInfo={x} editing={true} onDelete={handleOnDelete}/>)
                        )
                }

            </Grid>
        </Grid>
    )
}

export default observer(OrganiserContactsForm);