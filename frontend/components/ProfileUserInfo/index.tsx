import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { FC, useContext, useEffect, useState } from 'react';
import { StoreContext } from "../../stores/StoreContext";
import { IUser } from '../../@types/User';
import { Autocomplete, Divider, Grid, Stack, TextField } from '@mui/material';
import { DatePicker, LocalizationProvider } from '@mui/lab';
import AdapterDateFns from '@mui/lab/AdapterDateFns';

type Props = {
    user: IUser
}

const ProfileUserInfo: FC<Props> = (props: Props) => {
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

    //Component unmounts
    useEffect(() => {
        return () => {
            setCountries([]);
        }
    }, [])

    const handleFirstNameChange = (event) => {
        props.user.setFirstName(event.target.value)
    }

    const handleLastNameChange = (event) => {
        props.user.setLastName(event.target.value)
    }

    const handlePhoneNumberChange = (event) => {
        props.user.setPhoneNumber(event.target.value)
    }

    const handleDateOfBirthChange = (value) => {
        props.user.setDateOfBirth(value)
    }

    const handleCountryChange = (value) => {
        props.user.setCountry(value)
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
                        value={props.user.firstname}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        onChange={handleLastNameChange}
                        fullWidth={true}
                        label="Lastname"
                        id="profile-first-name"
                        value={props.user.lastname}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <Autocomplete
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
                        value={props.user.phonenumber}
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
                    <Button variant={"contained"}>
                        Save Changes
                    </Button>
                </Grid>
                <Grid item>
                    <Button onClick={() => stores.userStore.resetCurrentUser()}>Cancel</Button>
                </Grid>
            </Grid>

        </Stack>
    );
}


export default observer(ProfileUserInfo);