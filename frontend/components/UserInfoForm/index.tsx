import { Autocomplete, Avatar, Button, CircularProgress, Container, Divider, FormControl, Grid, InputLabel, List, ListItem, ListItemAvatar, ListItemText, MenuItem, Select, TextField, Typography } from "@mui/material";

import { FC, useContext, useEffect, useState } from "react";

import { observer } from "mobx-react-lite";
import { DatePicker, DateTimePicker, LoadingButton, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import styles from './styles.module.css'
import { IMarket } from "../../@types/Market";
import { StoreContext } from "../../stores/StoreContext";
import SaveIcon from '@mui/icons-material/Save';
import { computed } from "mobx";

type Props = {}

const UserInfoForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const [countries, setCountries] = useState([]);

    /**
     * Component will mount.
     * Load in all of the countries for the dropdown.
     */
    useEffect(() => {
        fetch('https://restcountries.com/v3.1/all')
            .then(response => response.json())
            .then(data => data.map(x => x.name.common))
            .then(names => setCountries(names));

    }, []);

    const handleFirstNameChange = (event) => {
        stores.userStore.newUser.setFirstName(event.target.value)
    }

    const handleLastNameChange = (event) => {
        stores.userStore.newUser.setLastName(event.target.value)
    }

    const handlePhoneNumberChange = (event) => {
        stores.userStore.newUser.setPhoneNumber(event.target.value)
    }

    const handleDateOfBirthChange = (value) => {
        stores.userStore.newUser.setDateOfBirth(value)
    }

    const handleCountryChange = (value) => {
        stores.userStore.newUser.setCountry(value)
    }

    return (
        <Grid container spacing={1}>
            <Grid item xs={6}>
                <TextField
                    id="inputFirstName"
                    label="First Name"
                    variant="outlined"
                    onChange={(event) => handleFirstNameChange(event)}
                    value={stores.userStore.newUser.firstname} />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    id="inputLastName"
                    label="Last Name"
                    variant="outlined"
                    onChange={(event) => handleLastNameChange(event)}
                    value={stores.userStore.newUser.lastname} />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    fullWidth={true}
                    id="inputPhoneNumber"
                    label="Phone Number"
                    variant="outlined"
                    onChange={(event) => handlePhoneNumberChange(event)}
                    value={stores.userStore.newUser.phonenumber} />
            </Grid>
            <Grid item xs={12}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DatePicker
                        label="Birthday"
                        inputFormat="MM/dd/yyyy"
                        value={stores.userStore.newUser.dateOfBirth}
                        onChange={(value) => handleDateOfBirthChange(value)}
                        renderInput={(params) => <TextField {...params} />}
                    />
                </LocalizationProvider>
            </Grid>
            <Grid item xs={12}>
                <Autocomplete
                    onChange={(event, value) =>handleCountryChange(value)}
                    freeSolo={false}
                    disablePortal
                    id="inputCountry"
                    options={countries}
                    renderInput={(params) => <TextField {...params} label="Country" />}
                />
            </Grid>
        </Grid>

    )
}

export default observer(UserInfoForm);