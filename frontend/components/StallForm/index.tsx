import { Container, Grid, IconButton, Input, Stack, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";

import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import ImageIcon from '@mui/icons-material/Image';
import WorkIcon from '@mui/icons-material/Work';
import BeachAccessIcon from '@mui/icons-material/BeachAccess';
import DeleteIcon from '@mui/icons-material/Delete';

type Props = {}



const StallForm: FC<Props> = (props: Props) => {

    return (
        <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
            <Grid container spacing={2} alignItems="center">
                <Grid item xs={8}>
                    <Typography variant="h6">
                        Basic Stall
                    </Typography>
                    <Typography variant="caption">
                        This is your generic basic stall (only type at the moment).
                    </Typography>
                </Grid>
                <Grid item xs={4}>
                    <Input type="number"/>
                </Grid>
            </Grid>
        </List>
    );
}


export default observer(StallForm);