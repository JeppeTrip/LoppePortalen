import SaveIcon from '@mui/icons-material/Save';
import { LoadingButton } from "@mui/lab";
import { Grid, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useCallback } from "react";
import { Location } from '../../@types/Location';
import { ModelState } from '../../@types/ModelState';
import { Organiser } from "../../NewStores/@DomainObjects/Organiser";
import RegionInput from '../RegionInput';
import styles from './styles.module.css';



type Props = {
    organiser: Organiser;
}

const OrganiserForm: FC<Props> = (props: Props) => {

    const handleOnLocationChange = useCallback((location : Location) => {
        props.organiser.location = location
    }, [props.organiser, props.organiser.location])
    
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
                <RegionInput value={props.organiser.location} onChange={handleOnLocationChange} />
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