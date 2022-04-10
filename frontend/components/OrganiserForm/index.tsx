import { Avatar, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, TextField, Typography } from "@mui/material";

import { FC, useContext, useEffect, useState } from "react";

import { observer } from "mobx-react-lite";
import { DateTimePicker, LoadingButton, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import styles from './styles.module.css'
import { IMarket } from "../../@types/Market";
import { StoreContext } from "../../stores/StoreContext";
import { IOrganiser } from "../../@types/Organiser";
import SaveIcon from '@mui/icons-material/Save';

type Props = {
    organiser: IOrganiser;
}

const OrganiserForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    return (
        <Grid container spacing={1}>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="organiserName"
                    label="Name"
                    variant="outlined"
                    value={props.organiser.name}
                    onChange={event => props.organiser.setName(event.target.value)}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="organiserStreet"
                    label="Street"
                    variant="outlined"
                    value={props.organiser.street}
                    onChange={event => props.organiser.setStreet(event.target.value)}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="organiserNumber"
                    label="Street Number"
                    variant="outlined"
                    value={props.organiser.streetNumber}
                    onChange={event => props.organiser.setStreetNumber(event.target.value)}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="organiserAppartment"
                    label="Appartment"
                    variant="outlined"
                    value={props.organiser.appartment}
                    onChange={event => props.organiser.setAppartment(event.target.value)}
                />
            </Grid>
            <Grid item xs={4}>
                <TextField
                    className={styles.nameInput}
                    id="organiserPostal"
                    label="Postal Code"
                    variant="outlined"
                    value={props.organiser.postalCode}
                    onChange={event => props.organiser.setPostalCode(event.target.value)}
                />
            </Grid>
            <Grid item xs={8}>
                <TextField
                    className={styles.nameInput}
                    id="organiserCity"
                    label="City"
                    variant="outlined"
                    value={props.organiser.city}
                    onChange={event => props.organiser.setCity(event.target.value)}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.descriptionInput}
                    id="outlined-multiline-static"
                    label="Description"
                    value={props.organiser.description}
                    onChange={event => props.organiser.setDescription(event.target.value)}
                    multiline
                    rows={10}
                />
            </Grid>
        </Grid>

    )
}

export default observer(OrganiserForm);