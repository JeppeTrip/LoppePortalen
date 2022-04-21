import { Avatar, Grid, ListItem, ListItemAvatar, ListItemButton, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Booth } from "../../NewStores/@DomainObjects/Booth";
import StorefrontIcon from '@mui/icons-material/Storefront';

type Props = {
    booth: Booth
}



const BoothListItem: FC<Props> = (props: Props) => {
    return (
        <ListItem>
            <ListItemButton onClick={() => console.log("click booth list item")}>
                <ListItemAvatar>
                    <Avatar>
                        <StorefrontIcon />
                    </Avatar>
                </ListItemAvatar>
                <Grid container spacing={2} alignItems="center">
                    <Grid item xs={8}>
                        <Stack>
                            <Typography
                                variant="h6">
                                {
                                    props.booth.name
                                }
                            </Typography>
                            <Grid container spacing={2}>
                                <Grid item>
                                    <Typography variant="caption">
                                        {
                                            props.booth.stall.market.name
                                        }
                                    </Typography>
                                </Grid>
                                <Grid item>
                                    <Typography variant="caption">
                                        {
                                            props.booth.stall.type.name
                                        }
                                    </Typography>
                                </Grid>
                            </Grid>

                        </Stack>
                    </Grid>
                </Grid>
            </ListItemButton>

        </ListItem>
    );
}


export default observer(BoothListItem);