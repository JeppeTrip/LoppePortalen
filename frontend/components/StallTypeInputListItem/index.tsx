import { Grid, IconButton, ListItem, Paper, Stack, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";

import { StoreContext } from "../../stores/StoreContext";
import { IStall, Stall } from "../../@types/Stall";
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import AddBoxIcon from '@mui/icons-material/AddBox';

type Props = {
    stall: IStall
}



const StallTypeInputListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    const handleOnAdd = (event) => {
        var added = stores.marketStore.newMarket.addStall(props.stall);
        if (added) {
            stores.stallFormUiStore.setIsAddingNewStall(false);
            stores.marketStore.newMarket.setNewStall();
        } else {
            stores.stallFormUiStore.setIsInvalidStall(true);
        }
    }

    const handleOnDelete = (event) => {
        stores.marketStore.newMarket.setNewStall()
        stores.stallFormUiStore.setIsAddingNewStall(false);
        stores.stallFormUiStore.setIsInvalidStall(false);
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
                                value={props.stall.type}
                                onChange={(event) => props.stall.setType(event.target.value)} />

                            <TextField
                                size="small"
                                id="stallDescInput"
                                label="Description"
                                variant="outlined"
                                value={props.stall.description}
                                onChange={(event) => props.stall.setDescription(event.target.value)} />
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
                        stores.stallFormUiStore.isInvalidStall &&
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