import { DateTimePicker, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import { FormControl, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useEffect } from "react";
import { Market } from "../../NewStores/@DomainObjects/Market";
import styles from './styles.module.css';





type Props = {
    market : Market
}

const MarketDetailsForm: FC<Props> = (props: Props) => {
    //TODO: Insert a loading indicator if the oragnisers doesn't exist in the store yet.
    //TODO: This will be a perforamnce nightmare if there ends up being a huge amount of organisers. this is likely temporary so will be fixed.
    useEffect(() => {
        props.market.store.rootStore.userStore.user.fetchOwnedOrganisers()
    }, []);
    
    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <FormControl fullWidth>
                    <InputLabel id="organiser-select">Organiser</InputLabel>
                    <Select
                        labelId="organiser-select"
                        id="organiser-select"
                        value={(props.market.organiser == null || props.market.organiser.id < 1) ? "" : props.market.organiser.id+""}
                        label="Organiser"
                        onChange={event => 
                            props.market.organiser = props.market.store.rootStore.userStore.user.organisers.find(x => x.id === parseInt(event.target.value))}
                    >
                        {
                            props.market.store.rootStore.userStore.user.organisers.map(o =>
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
                    onChange={(event) =>  props.market.name = event.target.value}
                    value={ props.market.name} />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="marketAddress"
                    label="Address"
                    variant="outlined"
                    onChange={(event) =>  props.market.name = event.target.value}
                    value={ props.market.name} />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="marketPostal"
                    label="Postal"
                    variant="outlined"
                    onChange={(event) =>  props.market.name = event.target.value}
                    value={ props.market.name} />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="marketCity"
                    label="City"
                    variant="outlined"
                    onChange={(event) =>  props.market.name = event.target.value}
                    value={ props.market.name} />
            </Grid>
            <Grid item xs={6}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DateTimePicker
                        renderInput={(props) => <TextField {...props} />}
                        label="Start Date"
                        value={ props.market.startDate}
                        onChange={(newValue) => {
                            props.market.startDate = newValue;
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
                            props.market.endDate = newValue;
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
                    onChange={(event) =>  props.market.description = event.target.value}
                    multiline
                    rows={10}
                />
            </Grid>
        </Grid>

    )
}

export default observer(MarketDetailsForm);