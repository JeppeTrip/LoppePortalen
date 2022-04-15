import { Grid, Input, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { StallType } from "../../NewStores/@DomainObjects/StallType";

type Props = {
    stallType: StallType
}



const StallTypeListItem: FC<Props> = (props: Props) => {
    const handleChange = (event) => {
        const newValue = event.target.value;
        const oldValue = props.stallType.stalls.length;
        const diff = newValue - oldValue;
        if(diff > 0)
        {
            props.stallType.addStalls(diff)
        }
    }
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
                        value={props.stallType.stalls.length}
                        onChange={handleChange}
                    />
                </Grid>
            </Grid>
        </ListItem>
    );
}


export default observer(StallTypeListItem);