import SaveIcon from '@mui/icons-material/Save';
import { LoadingButton } from "@mui/lab";
import { Grid, TextField } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Organiser } from "../../NewStores/@DomainObjects/Organiser";
import styles from './styles.module.css';



type Props = {
    organiser: Organiser;
}

const OrganiserForm: FC<Props> = (props: Props) => {

    return (
        <Grid container spacing={1}>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="organiserName"
                    label="Name"
                    variant="outlined"
                    value={props.organiser.name}
                    onChange={event => props.organiser.name = event.target.value}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.nameInput}
                    id="organiserStreet"
                    label="Street"
                    variant="outlined"
                    value={props.organiser.street}
                    onChange={event => props.organiser.street = event.target.value}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="organiserNumber"
                    label="Street Number"
                    variant="outlined"
                    value={props.organiser.streetNumber}
                    onChange={event => props.organiser.streetNumber = event.target.value}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    className={styles.nameInput}
                    id="organiserAppartment"
                    label="Appartment"
                    variant="outlined"
                    value={props.organiser.appartment}
                    onChange={event => props.organiser.appartment = event.target.value}
                />
            </Grid>
            <Grid item xs={4}>
                <TextField
                    className={styles.nameInput}
                    id="organiserPostal"
                    label="Postal Code"
                    variant="outlined"
                    value={props.organiser.postalCode}
                    onChange={event => props.organiser.postalCode = event.target.value}
                />
            </Grid>
            <Grid item xs={8}>
                <TextField
                    className={styles.nameInput}
                    id="organiserCity"
                    label="City"
                    variant="outlined"
                    value={props.organiser.city}
                    onChange={event => props.organiser.city = event.target.value}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    className={styles.descriptionInput}
                    id="outlined-multiline-static"
                    label="Description"
                    value={props.organiser.description}
                    onChange={event => props.organiser.description = event.target.value}
                    multiline
                    rows={10}
                />
            </Grid>
            <Grid item>
                <LoadingButton
                    onClick={() => props.organiser.save()}
                    loading={false}
                    loadingPosition="start"
                    startIcon={<SaveIcon />}
                    variant="contained"

                >
                    Submit
                </LoadingButton>
            </Grid>
        </Grid>

    )
}

export default observer(OrganiserForm);