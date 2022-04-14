import { Button, Grid, IconButton, Input, ListItem, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext, useEffect, useState } from "react";
import List from '@mui/material/List';
import AddIcon from '@mui/icons-material/Add';
import StallTypeListItem from "../StallTypeListItem";
import StallTypeInputListItem from "../StallTypeInputListItem";
import { Market } from "../../NewStores/@DomainObjects/Market";


type Props = {
    market: Market
}



const StallForm: FC<Props> = (props: Props) => {

    const handleOnClick = (event) => {
        props.market.createStall()

    }

    return (
        <Grid>
            <Grid item>
                <Button disabled={false} size="small" startIcon={<AddIcon />} onClick={handleOnClick}>
                    Add Stall
                </Button>
            </Grid>
            <Grid item xs={12}>
                <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
                    {
                        props.market.selectedStall != null && <StallTypeInputListItem stall={props.market.selectedStall} />
                    }
                    {
                        props.market.uniqueStalls.map(x => <StallTypeListItem stall={x} count={props.market.stallCount(x.name)} 
                        onChange={(event) => props.market.setNumberOfStalls(x.name, event.target.value === "" ? 0 : parseInt(event.target.value))}/>)
                    }
                </List>
            </Grid>
        </Grid>
    );
}


export default observer(StallForm);