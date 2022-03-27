import { Avatar, Button, CircularProgress, Container, Divider, FormControl, Grid, InputLabel, List, ListItem, ListItemAvatar, ListItemText, MenuItem, Select, TextField } from "@mui/material";

import { FC, useContext, useEffect, useState } from "react";

import { observer } from "mobx-react-lite";
import { DateTimePicker, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import styles from './styles.module.css'
import { IMarket } from "../../@types/Market";
import { StoreContext } from "../../stores/StoreContext";

type Props = {}

const MarketForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const [newMarket, setNewMarket] = useState<IMarket>(
        {
            id: -1,
            organiserId: -1,
            name: "",
            startDate: new Date(),
            endDate: new Date(),
            description: ""
        }
    );

    const handleUpdate = (key, value) => {
        console.log(key)
        console.log(value)
        setNewMarket(prevState => ({
            ...prevState,
            [key]: value
        }));
    }

    const handleSubmit = (event) => {
        stores.marketStore.addNewMarket(newMarket);
        setNewMarket({
            id: -1,
            organiserId: -1,
            name: "",
            startDate: new Date(),
            endDate: new Date(),
            description: ""
        });
    }

    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <FormControl fullWidth>
                    <InputLabel id="demo-simple-select-label">Organiser</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={newMarket.organiserId < 1 ? '' : newMarket.organiserId}
                        label="Organiser"
                        onChange={event => handleUpdate("organiserId", event.target.value as number)}
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
                    onChange={(event => handleUpdate("name", event.target.value))}
                    value={newMarket.name} />
            </Grid>
            <Grid item xs={6}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DateTimePicker
                        renderInput={(props) => <TextField {...props} />}
                        label="Start Date"
                        value={newMarket.startDate}
                        onChange={(newValue) => {
                            handleUpdate("startDate", newValue)
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
                        value={newMarket.endDate}
                        onChange={(newValue) => {
                            handleUpdate("endDate", newValue)
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
                    value={newMarket.description}
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

export default MarketForm;