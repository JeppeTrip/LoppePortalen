import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import { CircularProgress, Grid, IconButton, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { ModelState } from "../../@types/ModelState";
import { Stall } from "../../NewStores/@DomainObjects/Stall";
type Props = {
    stall: Stall
}



const StallListItem: FC<Props> = (props: Props) => {
    return (
        <ListItem
            secondaryAction={
                props.stall.state === ModelState.UPDATING ?
                    (<CircularProgress />)
                    :
                    (<IconButton edge="end"
                        disabled={props.stall.state === ModelState.NEW || props.stall.booth != null}
                        onClick={() => props.stall.delete()}><DeleteForeverIcon />
                    </IconButton>)
            }
        >
            <Grid container spacing={2} sx={{ flexirection: "row" }} alignItems="center">
                <Grid item>
                    <Typography>
                        {props.stall.id}
                    </Typography>
                </Grid>
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
        </ListItem >
    );
}


export default observer(StallListItem);