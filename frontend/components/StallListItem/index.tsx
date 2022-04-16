import { Grid, Input, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Stall } from "../../NewStores/@DomainObjects/Stall";
import { StallType } from "../../NewStores/@DomainObjects/StallType";

type Props = {
    stall: Stall
}



const StallListItem: FC<Props> = (props: Props) => {
    return (
        <ListItem>
            <Grid container spacing={2} alignItems="center">
                <Grid item xs={8}>
                    <Stack>
                        <Typography
                            variant="h6">
                            {
                                props.stall.type.name
                            }
                        </Typography>
                        <Typography variant="caption">
                            {
                                props.stall.type.description
                            }
                        </Typography>
                    </Stack>
                </Grid>
            </Grid>
        </ListItem>
    );
}


export default observer(StallListItem);