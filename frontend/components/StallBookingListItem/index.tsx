import { Button, ButtonGroup, Grid, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { StallType } from '../../NewStores/@DomainObjects/StallType';

type Props = {
    stallType: StallType,
    secondary?
}



const StallBookingListItem: FC<Props> = (props: Props) => {
    return (
        <ListItem
            key={props.stallType.id}
            secondaryAction={props.secondary ? props.secondary : null}
        >
            <Grid container spacing={2} alignItems="center">
                <Grid item xs={8}>
                    <Stack>
                        <Typography
                            variant="h6">
                            {
                                props.stallType.name
                            }
                        </Typography>
                        <Typography variant="caption">
                            {
                                props.stallType.description
                            }
                        </Typography>
                    </Stack>
                </Grid>
            </Grid>
        </ListItem>
    );
}


export default observer(StallBookingListItem);