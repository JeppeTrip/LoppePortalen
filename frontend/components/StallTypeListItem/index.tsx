import { Grid, Input, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";
import { StoreContext } from "../../stores/StoreContext";
import { Stall } from "../../NewStores/@DomainObjects/Stall";

type Props = {
    stall: Stall,
    count: number,
    onChange: any
}



const StallTypeListItem: FC<Props> = (props: Props) => {

    return (
        <ListItem>
            <Grid container spacing={2} alignItems="center">
                <Grid item xs={8}>
                    <Stack>
                        <Typography
                            variant="h6">
                            {
                                props.stall.name
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