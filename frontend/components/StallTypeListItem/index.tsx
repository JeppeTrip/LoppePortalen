import { Button, Grid, IconButton, Input, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext, useState } from "react";

import List from '@mui/material/List';
import AddIcon from '@mui/icons-material/Add';
import { StoreContext } from "../../stores/StoreContext";
import { IStall } from "../../@types/Stall";

type Props = {
    stall: IStall
}



const StallTypeListItem: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    return (
        <ListItem>
            <Grid container spacing={2} alignItems="center">
                <Grid item xs={8}>
                    <Stack>
                        <Typography
                            variant="h6">
                            {
                                props.stall.type
                            }
                        </Typography>
                        <Typography variant="caption">
                            {
                                props.stall.description
                            }
                        </Typography>
                    </Stack>

                </Grid>
                <Grid item xs={4}>
                    <Input
                        type="number"
                        value={stores.marketStore.newMarket.stallCount(props.stall.type)}
                        onChange={(event) => { console.log(event.target.value); stores.marketStore.newMarket.setNumberOfStalls(props.stall.type, event.target.value === "" ? 0 : parseInt(event.target.value)) }}
                    />
                </Grid>
            </Grid>
        </ListItem>
    );
}


export default observer(StallTypeListItem);