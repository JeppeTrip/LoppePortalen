import { Avatar, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, TextField } from "@mui/material";

import { FC, useContext, useEffect, useState } from "react";

import { observer } from "mobx-react-lite";
import { DateTimePicker, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import styles from './styles.module.css'
import { IMarket } from "../../@types/Market";
import { StoreContext } from "../../stores/StoreContext";
import { IOrganiser } from "../../@types/Organiser";

type Props = {}

const OrganiserForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const [newOrganiser, setNewOrganiser] = useState<IOrganiser>(
        {
            id: undefined,
            name: "",
            description: "",
            street: "",
            streetNumber: "",
            appartment: "",
            postalCode: "",
            city: ""
        }
    );

    const handleUpdate = (key, value) => {
        setNewOrganiser(prevState => ({
            ...prevState,
            [key]: value
        }));
    }

    const handleSubmit = (event) => {
        stores.organiserStore.addOrganiser(newOrganiser);
        setNewOrganiser({
            id: undefined,
            name: "",
            description: "",
            street: "",
            streetNumber: "",
            appartment: "",
            postalCode: "",
            city: ""
        });
    }

    return (
        <Grid container spacing={1}>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="organiserName"
                    label="Name"
                    variant="outlined"
                    value={newOrganiser.name}
                    onChange={(event => handleUpdate("name", event.target.value))}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="organiserStreet"
                    label="Street"
                    variant="outlined"
                    value={newOrganiser.street}
                    onChange={(event => handleUpdate("street", event.target.value))}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="organiserNumber"
                    label="Street Number"
                    variant="outlined"
                    value={newOrganiser.streetNumber}
                    onChange={(event => handleUpdate("streetNumber", event.target.value))}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="organiserAppartment"
                    label="Appartment"
                    variant="outlined"
                    value={newOrganiser.appartment}
                    onChange={(event => handleUpdate("appartment", event.target.value))}
                />
            </Grid>
            <Grid item xs={4}>
                <TextField
                    className={styles.nameInput}
                    id="organiserPostal"
                    label="Postal Code"
                    variant="outlined"
                    value={newOrganiser.postalCode}
                    onChange={(event => handleUpdate("postalCode", event.target.value))}
                />
            </Grid>
            <Grid item xs={8}>
                <TextField
                    className={styles.nameInput}
                    id="organiserCity"
                    label="City"
                    variant="outlined"
                    value={newOrganiser.city}
                    onChange={(event => handleUpdate("city", event.target.value))}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.descriptionInput}
                    id="outlined-multiline-static"
                    label="Description"
                    value={newOrganiser.description}
                    onChange={(event => handleUpdate("description", event.target.value))}
                    multiline
                    rows={10}
                />
            </Grid>
            <Grid item>
                <Button variant="contained" onClick={handleSubmit}>Submit</Button>
            </Grid>
        </Grid>

    )
}

export default OrganiserForm;