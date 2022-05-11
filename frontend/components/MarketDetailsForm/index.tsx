import { DateTimePicker, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import { Autocomplete, CircularProgress, FormControl, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useEffect, useState } from "react";
import { Market } from "../../NewStores/@DomainObjects/Market";
import styles from './styles.module.css';


interface Postal {
    number: number;
    name: string;
  }


type Props = {
    market: Market
}

const MarketDetailsForm: FC<Props> = (props: Props) => {
    //TODO: Insert a loading indicator if the oragnisers doesn't exist in the store yet.
    //TODO: This will be a perforamnce nightmare if there ends up being a huge amount of organisers. this is likely temporary so will be fixed.
    useEffect(() => {
        props.market.store.rootStore.userStore.user.fetchOwnedOrganisers()
    }, []);

    const [open, setOpen] = useState(false);
    const [postals, setPostals] = useState<readonly Postal[]>([]);
    const loadingPostal = open && postals.length === 0;

    useEffect(() => {
        let active = true;
    
        if (!loadingPostal) {
          return undefined;
        }
    
        (async () => {
          if (active) {
            fetch('https://api.dataforsyningen.dk/postnumre')
            .then(response => response.json())
            .then(rawData => rawData.map(x => {
                const p = {number: x.nr, name: x.navn} as Postal
                return p
            }))
            .then(data => {
                setPostals([...data])
            });
          }
        })();
    
        return () => {
          active = false;
        };
      }, [loadingPostal]);

    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <FormControl fullWidth>
                    <InputLabel id="organiser-select">Organiser</InputLabel>
                    <Select
                        labelId="organiser-select"
                        id="organiser-select"
                        value={(props.market.organiser == null || props.market.organiser.id < 1) ? "" : props.market.organiser.id + ""}
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
                    onChange={(event) => props.market.name = event.target.value}
                    value={props.market.name} />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="marketAddress"
                    label="Address"
                    variant="outlined"
                    onChange={(event) => props.market.address = event.target.value}
                    value={props.market.address} />
            </Grid>
            <Grid item xs={6}>
                <Autocomplete
                    id="postals-autocomplete"
                    open={open}
                    onOpen={() => {
                        setOpen(true);
                    }}
                    onClose={() => {
                        setOpen(false);
                    }}
                    isOptionEqualToValue={(option, value) => option.number === value.number}
                    getOptionLabel={(postal) => postal.number}
                    options={postals}
                    loading={loadingPostal}
                    value={props.market.postalCode}
                    onChange={(event,value) => {
                        if(!value || value == null)
                        {
                            props.market.postalCode = null; 
                            props.market.city = null
                        }
                        else
                        {
                            props.market.postalCode = value.number; 
                            props.market.city = value.name
                        }
                    }}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            label="Postal"
                            InputProps={{
                                ...params.InputProps,
                                endAdornment: (
                                    <>
                                        {loadingPostal ? <CircularProgress color="inherit" size={20} /> : null}
                                        {params.InputProps.endAdornment}
                                    </>
                                ),
                            }}
                        />
                    )}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    disabled
                    className={styles.nameInput}
                    id="marketCity"
                    label="City"
                    variant="outlined"
                    value={props.market.city ? props.market.city : ""} />
            </Grid>
            <Grid item xs={6}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DateTimePicker
                        renderInput={(props) => <TextField {...props} />}
                        label="Start Date"
                        value={props.market.startDate}
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
                        value={props.market.endDate}
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
                    value={props.market.description}
                    onChange={(event) => props.market.description = event.target.value}
                    multiline
                    rows={10}
                />
            </Grid>
        </Grid>

    )
}

export default observer(MarketDetailsForm);