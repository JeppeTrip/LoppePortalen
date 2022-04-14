import AddBoxIcon from '@mui/icons-material/AddBox';
import DeleteIcon from '@mui/icons-material/Delete';
import { Grid, IconButton, ListItem, Paper, Stack, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";
import { StallType } from "../../NewStores/@DomainObjects/StallType";
import { StoreContext } from "../../NewStores/StoreContext";


type Props = {
    stallType: StallType
}



const StallTypeInputListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    const handleOnAdd = (event) => {
        props.stallType.save()
        props.stallType.deselect()
    }

    const handleOnDelete = (event) => {

    }

    return (
        <ListItem>
            <Paper elevation={1} sx={{ px: 1, paddingBottom: 1 }}>
                <Grid container spacing={1} alignItems="center">
                    <Grid item xs={12} />
                    <Grid item xs={8}>
                        <Stack spacing={1}>
                            <TextField
                                size="small"
                                id="stallNameInput"
                                label="Name"
                                variant="outlined"
                                value={props.stallType.name}
                                onChange={(event) => props.stallType.name = event.target.value} />

                            <TextField
                                size="small"
                                id="stallDescInput"
                                label="Description"
                                variant="outlined"
                                value={props.stallType.description}
                                onChange={(event) => props.stallType.description = event.target.value} />
                        </Stack>

                    </Grid>
                    <Grid container item xs={4} justifyContent="end" >
                        <IconButton
                            color="success"
                            aria-label="stallAdd"
                            onClick={handleOnAdd}>
                            <AddBoxIcon />
                        </IconButton>
                        <IconButton
                            aria-label="stallDelete"
                            onClick={handleOnDelete}>
                            <DeleteIcon />
                        </IconButton>
                    </Grid>
                    {
                        //TODO: Make error handling waaay the fuck better.
                        props.stallType.name === "" || props.stallType.description === "" &&
                        <Grid item xs={12}>
                            <Typography variant="caption" color={"red"}>
                                Stall must have a unique name, and a description.
                            </Typography>
                        </Grid>
                    }
                </Grid>
            </Paper>

        </ListItem>
    );
}


export default observer(StallTypeInputListItem);