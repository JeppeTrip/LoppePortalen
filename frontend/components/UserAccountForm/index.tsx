import { Avatar, Button, CircularProgress, Container, Divider, FormControl, Grid, InputLabel, List, ListItem, ListItemAvatar, ListItemText, MenuItem, Select, TextField, Typography } from "@mui/material";

import { FC, useContext, useEffect, useState } from "react";

import { observer } from "mobx-react-lite";
import { DateTimePicker, LoadingButton, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import styles from './styles.module.css'
import { IMarket } from "../../@types/Market";
import { StoreContext } from "../../stores/StoreContext";
import SaveIcon from '@mui/icons-material/Save';
import { computed } from "mobx";

type Props = {}

const UserAccountForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    
    const handleEmailChange = (event) => {
        stores.userStore.newUser.setEmail(event.target.value)
    }

    const handlePasswordChange =(event) => {
        stores.userStore.newUser.setPassword(event.target.value)
    }

    return (
        <Grid container spacing={1}>
            <Grid item xs={12}>
                <TextField
                    value={stores.userStore.newUser.email}
                    onChange={event => handleEmailChange(event)}
                    required
                    fullWidth
                    id="email"
                    label="Email Address"
                    name="email"
                    autoComplete="email"
                    autoFocus />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    required
                    fullWidth
                    name="password"
                    label="Password"
                    type="password"
                    id="password"
                    autoComplete="current-password"
                    value={stores.userStore.newUser.password}
                    onChange={event => handlePasswordChange(event)} />
            </Grid>
        </Grid >

    )
}

export default observer(UserAccountForm);