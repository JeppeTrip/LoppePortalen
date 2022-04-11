import { Button, Grid, IconButton, Input, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext, useState } from "react";

import List from '@mui/material/List';
import AddIcon from '@mui/icons-material/Add';
import { StoreContext } from "../../stores/StoreContext";
import { IStall } from "../../@types/Stall";

type Props = {
    stall: IStall,
    count: number,
    onChange: any
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
                        value={props.count}
                        onChange={props.onChange}
                    />
                </Grid>
            </Grid>
        </ListItem>
    );
}


export default observer(StallTypeListItem);