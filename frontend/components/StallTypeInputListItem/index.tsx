import { Grid, IconButton, ListItem, Paper, Stack, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";

import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import AddBoxIcon from '@mui/icons-material/AddBox';
import { Stall } from "../../NewStores/@DomainObjects/Stall";
import { StoreContext } from "../../NewStores/StoreContext";

type Props = {
    stall: Stall
}



const StallTypeInputListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    const handleOnAdd = (event) => {
        stores.marketStore.selectedMarket.stalls.push(stores.marketStore.selectedMarket.selectedStall)
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
                                value={props.stall.name}
                                onChange={(event) => props.stall.name = event.target.value} />

                            <TextField
                                size="small"
                                id="stallDescInput"
                                label="Description"
                                variant="outlined"
                                value={props.stall.description}
                                onChange={(event) => props.stall.description = event.target.value} />
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
                        props.stall.name === "" || props.stall.description === "" &&
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