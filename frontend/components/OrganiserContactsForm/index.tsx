import AddIcon from '@mui/icons-material/Add';
import { Button, Grid, List } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useEffect, useState } from "react";
import { ContactInfo } from '../../NewStores/@DomainObjects/ContactInfo';
import { Organiser } from "../../NewStores/@DomainObjects/Organiser";
import ContactInfoInputListItem from "../../components/ContactInfoNewListItem";
import { ModelState } from '../../@types/ModelState';
import ContactInfoListItem from '../ContactInfoListItem';


type Props = {
    organiser: Organiser;
}

const OrganiserContactsForm: FC<Props> = (props: Props) => {
    const [newContact, setNewContact] = useState<ContactInfo>(null)

    useEffect(() => {
        if(newContact != null && newContact.state === ModelState.IDLE)
            setNewContact(null)
    }, [newContact?.state])

    return (
        <Grid>
            <Grid item>
                <Button disabled={newContact != null} size="small" startIcon={<AddIcon />} onClick={() => {
                    setNewContact(new ContactInfo(props.organiser))
                }}>
                    Add Contact Information
                </Button>
            </Grid>
            <Grid item>
                <List>
                    {
                        newContact != null && <ContactInfoInputListItem contactInfo={newContact}/>
                    }
                    
                </List>
                {
                    props.organiser.contactInfo.length === 0 ?
                        (
                            "no contact info found"
                        )
                        :
                        (
                            props.organiser.contactInfo.map(x => <ContactInfoListItem key={`${props.organiser.id}_${x.value}`} contactInfo={x}/>)
                        )
                }

            </Grid>
        </Grid>
    )
}

export default observer(OrganiserContactsForm);