import { DatePicker, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import { Autocomplete, Grid, TextField } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useEffect, useState } from "react";
import { User } from "../../NewStores/@DomainObjects/User";
import styles from "./styles.module.css";



type Props = {
    user: User
}

const UserInfoForm: FC<Props> = (props: Props) => {
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
        props.user.firstName = event.target.value
    }

    const handleLastNameChange = (event) => {
        props.user.lastName = event.target.value
    }

    const handlePhoneNumberChange = (event) => {
        props.user.phoneNumber = event.target.value
    }

    const handleDateOfBirthChange = (value) => {
        props.user.dateOfBirth = value
    }

    const handleCountryChange = (value) => {
        props.user.country = value
    }

    return (
        <Grid container spacing={1}>
            <Grid item xs={6}>
                <TextField
                    fullWidth={true}
                    id="inputFirstName"
                    label="First Name"
                    variant="outlined"
                    onChange={(event) => handleFirstNameChange(event)}
                    value={props.user.firstName} />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    fullWidth={true}
                    id="inputLastName"
                    label="Last Name"
                    variant="outlined"
                    onChange={(event) => handleLastNameChange(event)}
                    value={props.user.lastName} />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    fullWidth={true}
                    id="inputPhoneNumber"
                    label="Phone Number"
                    variant="outlined"
                    onChange={(event) => handlePhoneNumberChange(event)}
                    value={props.user.phoneNumber} />
            </Grid>
            <Grid item xs={12}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DatePicker
                        className={styles.fullWidthDatePicker}
                        label="Birthday"
                        inputFormat="MM/dd/yyyy"
                        value={props.user.dateOfBirth}
                        onChange={(value) => handleDateOfBirthChange(value)}
                        renderInput={(params) => <TextField {...params} />}
                    />
                </LocalizationProvider>
            </Grid>
            <Grid item xs={12}>
                <Autocomplete
                    onChange={(event, value) => handleCountryChange(value)}
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