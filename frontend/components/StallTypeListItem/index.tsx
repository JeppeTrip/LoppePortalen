import { Grid, Input, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { StallType } from "../../NewStores/@DomainObjects/StallType";

type Props = {
    stallType: StallType
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
                <Grid item xs={4}>
                    <Input
                        type="number"
                        value={0}
                        onChange={() => console.log("changed")}
                    />
                </Grid>
            </Grid>
        </ListItem>
    );
}


export default observer(StallTypeListItem);