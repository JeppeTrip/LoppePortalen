import { Grid, ListItem, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { IStall } from "../../@types/Stall";


type Props = {
    stall: IStall,
    available: number,
    total: number
}

const StallTypeInfoListItem: FC<Props> = (props: Props) => {
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
                <Grid container item xs={4}>
                    <Grid item xs={12}>
                        <Typography variant="caption">
                            {`Available ${props.available}`}
                        </Typography>

                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="caption">
                            {`Total ${props.total}`}
                        </Typography>

                    </Grid>
                </Grid>
            </Grid>
        </ListItem>
    );
}


export default observer(StallTypeInfoListItem);