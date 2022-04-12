import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { FC, useEffect, useState } from 'react';
import { Autocomplete, Divider, Grid, Stack, TextField } from '@mui/material';
import { DatePicker, LocalizationProvider } from '@mui/lab';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import { User } from '../../NewStores/@DomainObjects/User';

type Props = {
    user: User
}

const ProfileUserInfo: FC<Props> = (props: Props) => {
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

    //Component unmounts
    useEffect(() => {
        return () => {
            setCountries([]);
        }
    }, [])

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

    }

    return (
        <Stack spacing={1} >
            <Typography variant="h2">
                User Info
            </Typography>
            <Divider />
            <Grid container spacing={1}>
                <Grid item xs={12}>
                    <TextField
                        disabled={true}
                        fullWidth={true}
                        label="Email"
                        id="profile-first-name"
                        value={props.user.email}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        onChange={handleFirstNameChange}
                        fullWidth={true}
                        label="Firstname"
                        id="profile-first-name"
                        value={props.user.firstName}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        onChange={handleLastNameChange}
                        fullWidth={true}
                        label="Lastname"
                        id="profile-first-name"
                        value={props.user.lastName}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <Autocomplete
                        value={props.user.country}
                        onChange={(event, value) => handleCountryChange(value)}
                        freeSolo={false}
                        disablePortal
                        id="inputCountry"
                        options={countries}
                        renderInput={(params) => <TextField {...params} label="Country" />}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        onChange={handlePhoneNumberChange}
                        fullWidth={true}
                        label="Phone number"
                        id="profile-phone-number"
                        value={props.user.phoneNumber}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                        <DatePicker
                            onChange={(value) => { handleDateOfBirthChange(value) }}
                            label="Birthday"
                            inputFormat="MM/dd/yyyy"
                            value={props.user.dateOfBirth}
                            renderInput={(params) => <TextField {...params} />}
                        />
                    </LocalizationProvider>
                </Grid>
            </Grid>

            <Grid container spacing={1}>
                <Grid item>
                    <Button 
                        variant={"contained"}
                        onClick={() => props.user.save()}>
                        Save Changes
                    </Button>
                </Grid>
                <Grid item>
                    <Button onClick={() => props.user.resetChanges()}>Cancel</Button>
                </Grid>
            </Grid>

        </Stack>
    );
}


export default observer(ProfileUserInfo);