import { Button, Grid, IconButton, Input, ListItem, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext, useState } from "react";

import List from '@mui/material/List';
import AddIcon from '@mui/icons-material/Add';
import { StoreContext } from "../../stores/StoreContext";
import StallTypeListItem from "../StallTypeListItem";
import StallTypeInputListItem from "../StallTypeInputListItem";

type Props = {}



const StallForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    
    const handleOnClick = (event) => {
        stores.stallFormUiStore.setIsAddingNewStall(true);
        stores.marketStore.newMarket.setNewStall()
    }

    return (
        <Grid>
            <Grid item>
                <Button disabled={stores.stallFormUiStore.isAddingNewStall} size="small" startIcon={<AddIcon />} onClick={handleOnClick}>
                    Add Stall
                </Button>
            </Grid>
            <Grid item xs={12}>
                <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
                    {
                        stores.stallFormUiStore.isAddingNewStall && 
                        <StallTypeInputListItem stall={stores.marketStore.newMarket.newStall} />
                    }
                    {
                        stores.marketStore.newMarket.uniqueStalls.map(x => <StallTypeListItem stall={x}/>)
                    }
                </List>
            </Grid>

        </Grid>

    );
}


export default observer(StallForm);