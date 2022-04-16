import SaveIcon from '@mui/icons-material/Save';
import { LoadingButton } from "@mui/lab";
import { Button, Grid, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useEffect } from "react";
import { ModelState } from '../../@types/ModelState';
import { Organiser } from "../../NewStores/@DomainObjects/Organiser";
import styles from './styles.module.css';



type Props = {
    organiser: Organiser;
}

const OrganiserForm: FC<Props> = (props: Props) => {
    /**
     * Placed here because I'm not going to figure out how to do this from the page right 
     * now. Hopefully I will get back to this.
     */
    useEffect(() => {
        return () => {
            if(props.organiser.state === ModelState.EDITING)
            {
                props.organiser.state = ModelState.IDLE
            }
        }
    },[])

    return (
        <Grid container spacing={1}>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="organiserName"
                    label="Name"
                    variant="outlined"
                    value={props.organiser.name}
                    onChange={event => props.organiser.setName = event.target.value}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="organiserStreet"
                    label="Street"
                    variant="outlined"
                    value={props.organiser.street}
                    onChange={event => props.organiser.setStreet = event.target.value}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="organiserNumber"
                    label="Street Number"
                    variant="outlined"
                    value={props.organiser.streetNumber}
                    onChange={event => props.organiser.setStreetNumber = event.target.value}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="organiserAppartment"
                    label="Appartment"
                    variant="outlined"
                    value={props.organiser.appartment}
                    onChange={event => props.organiser.setAppartment = event.target.value}
                />
            </Grid>
            <Grid item xs={4}>
                <TextField
                    className={styles.nameInput}
                    id="organiserPostal"
                    label="Postal Code"
                    variant="outlined"
                    value={props.organiser.postalCode}
                    onChange={event => props.organiser.setPostalCode = event.target.value}
                />
            </Grid>
            <Grid item xs={8}>
                <TextField
                    className={styles.nameInput}
                    id="organiserCity"
                    label="City"
                    variant="outlined"
                    value={props.organiser.city}
                    onChange={event => props.organiser.setCity = event.target.value}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.descriptionInput}
                    id="outlined-multiline-static"
                    label="Description"
                    value={props.organiser.description}
                    onChange={event => props.organiser.setDescription = event.target.value}
                    multiline
                    rows={10}
                />
            </Grid>
            <Grid item>
                <LoadingButton
                    onClick={() => props.organiser.save()}
                    loading={props.organiser.state === ModelState.SAVING}
                    loadingPosition="start"
                    startIcon={<SaveIcon />}
                    variant="contained"

                >
                    Submit
                </LoadingButton>
            </Grid>
            {
                (props.organiser.state === ModelState.ERROR) &&
                <Grid item>
                    <Typography variant="caption" color={"red"}>
                        Something went wrong.
                        Could not submit organiser
                    </Typography>
                </Grid>


            }
        </Grid>

    )
}

export default observer(OrganiserForm);