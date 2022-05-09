import AddBoxIcon from '@mui/icons-material/AddBox';
import DeleteIcon from '@mui/icons-material/Delete';
import { CircularProgress, FormControl, Grid, IconButton, InputLabel, ListItem, MenuItem, Paper, Select, Stack, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { ModelState } from '../../@types/ModelState';
import { ContactInfo } from '../../NewStores/@DomainObjects/ContactInfo';
import { ContactInfoType } from '../../services/clients';


type Props = {
    contactInfo: ContactInfo
    onAdd
}

const ContactInfoInputListItem: FC<Props> = (props: Props) => {
    /** This is a little bit hacky but component is rendered in parent when model state is new,
     * and since it only exists in the parent component state, we can just have the parent component
     * react to when the modelstate isn't new, and then remove the input list component from the list.
     */
    const handleOnDelete = (event) => {
        props.contactInfo.state = ModelState.IDLE
    }

    return (
        <ListItem>
            <Paper elevation={1} sx={{ px: 1, paddingBottom: 1 }}>
                <Grid container spacing={1} alignItems="center">
                    <Grid item xs={12} />
                    <Grid item xs={4} >
                        <FormControl sx={{ m: 1, minWidth: 120 }} size="small">
                            <InputLabel id="demo-select-small">Age</InputLabel>
                            <Select
                                labelId="demo-select-small"
                                id="demo-select-small"
                                value={props.contactInfo.type}
                                label="Type"
                                onChange={(event) => props.contactInfo.type = event.target.value as ContactInfoType}
                            >
                                <MenuItem value={ContactInfoType.EMAIL}>Mail</MenuItem>
                                <MenuItem value={ContactInfoType.FACEBOOK}>Facebook</MenuItem>
                                <MenuItem value={ContactInfoType.INSTAGRAM}>Instagram</MenuItem>
                                <MenuItem value={ContactInfoType.PHONE_NUMER}>Phone</MenuItem>
                                <MenuItem value={ContactInfoType.TIKTOK}>TikTok</MenuItem>
                                <MenuItem value={ContactInfoType.TWITTER}>Twitter</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={8}>
                        <Stack spacing={1}>
                            <TextField
                                size="small"
                                id="contactInfoValue"
                                variant="outlined"
                                value={props.contactInfo.value}
                                onChange={(event) => props.contactInfo.value = event.target.value} />
                        </Stack>

                    </Grid>
                    <Grid item>
                        {
                            props.contactInfo.state === ModelState.SAVING ?
                                (
                                    <CircularProgress />
                                )
                                :
                                (
                                    <>
                                        <IconButton
                                            color="success"
                                            aria-label="contactAdd"
                                            onClick={props.onAdd}>
                                            <AddBoxIcon />
                                        </IconButton>
                                        <IconButton
                                            aria-label="contactCancel"
                                            onClick={handleOnDelete}>
                                            <DeleteIcon />
                                        </IconButton>
                                    </>

                                )
                        }

                    </Grid>
                    {
                        //TODO: Make error handling waaay the fuck better.
                        props.contactInfo.state === ModelState.ERROR &&
                        <Grid item xs={12}>
                            <Typography variant="caption" color={"red"}>
                                Contact info must be unique for the organiser.
                            </Typography>
                        </Grid>
                    }
                </Grid>
            </Paper>

        </ListItem>
    );
}


export default observer(ContactInfoInputListItem);