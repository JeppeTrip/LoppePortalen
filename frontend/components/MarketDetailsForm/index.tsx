import { Avatar, Button, CircularProgress, Container, Divider, FormControl, Grid, InputLabel, List, ListItem, ListItemAvatar, ListItemText, MenuItem, Select, TextField, Typography } from "@mui/material";

import { FC, useContext, useEffect, useState } from "react";

import { observer } from "mobx-react-lite";
import { DateTimePicker, LoadingButton, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import styles from './styles.module.css'
import { IMarket, Market } from "../../@types/Market";
import { StoreContext } from "../../stores/StoreContext";
import SaveIcon from '@mui/icons-material/Save';
import { computed } from "mobx";

type Props = {
    market : Market
}

const MarketDetailsForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    //TODO: Insert a loading indicator if the oragnisers doesn't exist in the store yet.
    //TODO: This will be a perforamnce nightmare if there ends up being a huge amount of organisers. this is likely temporary so will be fixed.
    useEffect(() => {
        if (stores.organiserStore.organisers.length === 0) {
            stores.organiserStore.loadOrganisers()
        }
    }, []);
    
    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <FormControl fullWidth>
                    <InputLabel id="demo-simple-select-label">Organiser</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={props.market.organiserId < 1 ? '' : props.market.organiserId}
                        label="Organiser"
                        onChange={event => props.market.setOrganiserId(event.target.value as number)}
                    >
                        {
                            stores.organiserStore.organisers.map(o =>
                                <MenuItem value={o.id}>{o.name}</MenuItem>
                            )
                        }
                    </Select>
                </FormControl>

            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="marketName"
                    label="Name"
                    variant="outlined"
                    onChange={(event) =>  props.market.setName(event.target.value)}
                    value={ props.market.name} />
            </Grid>
            <Grid item xs={6}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DateTimePicker
                        renderInput={(props) => <TextField {...props} />}
                        label="Start Date"
                        value={ props.market.startDate}
                        onChange={(newValue) => {
                            props.market.setStartDate(newValue);
                        }
                        }
                    />
                </LocalizationProvider>
            </Grid>
            <Grid item xs={6}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DateTimePicker
                        renderInput={(props) => <TextField {...props} />}
                        label="End Date"
                        value={ props.market.endDate}
                        onChange={(newValue) => {
                            props.market.setEndDate(newValue);
                        }
                        }
                    />
                </LocalizationProvider>
            </Grid>

            <Grid item xs={12}>
                <TextField
                    className={styles.descriptionInput}
                    id="outlined-multiline-static"
                    label="Description"
                    value={ props.market.description}
                    onChange={(event) =>  props.market.setDescription(event.target.value)}
                    multiline
                    rows={10}
                />
            </Grid>
        </Grid>

    )
}

export default observer(MarketDetailsForm);