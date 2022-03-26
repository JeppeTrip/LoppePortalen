import { Avatar, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, TextField } from "@mui/material";

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
            id: undefined,
            organiserId: undefined,
            name: "",
            startDate: new Date(),
            endDate: new Date(),
            description: ""
        }
    );

    const handleUpdate = (key, value) => {
        setNewMarket(prevState => ({
            ...prevState,
            [key]: value
        }));
    }

    const handleSubmit = (event) => {
        stores.marketStore.addNewMarket(newMarket);
        setNewMarket({
            id: undefined,
            organiserId: undefined,
            name: "",
            startDate: new Date(),
            endDate: new Date(),
            description: ""
        });
    }

    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                {
                    //TODO: Fix this, this bad.
                }
                <TextField
                    className={styles.nameInput}
                    id="organiserId"
                    label="Organiser ID"
                    variant="outlined"
                    value={newMarket.organiserId}
                    type="number"
                    onChange={(event => handleUpdate("organiserId", parseInt(event.target.value)))}
                />
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