import { Button, Grid, IconButton, Input, ListItem, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext, useState } from "react";

import List from '@mui/material/List';
import AddIcon from '@mui/icons-material/Add';
import { StoreContext } from "../../stores/StoreContext";
import StallTypeListItem from "../StallTypeListItem";
import StallTypeInputListItem from "../StallTypeInputListItem";
import { Market } from "../../@types/Market";

type Props = {
    market: Market
}



const StallForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    const handleOnClick = (event) => {
        stores.stallFormUiStore.setIsAddingNewStall(true);
        props.market.setNewStall()
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
                        <StallTypeInputListItem stall={props.market.newStall} />
                    }
                    {
                        props.market.uniqueStalls().map(x => <StallTypeListItem stall={x} count={props.market.stallCount(x.type)} 
                        onChange={(event) => props.market.setNumberOfStalls(x.type, event.target.value === "" ? 0 : parseInt(event.target.value))}/>)
                    }
                </List>
            </Grid>

        </Grid>

    );
}


export default observer(StallForm);