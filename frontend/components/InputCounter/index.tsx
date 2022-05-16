import { Button, ButtonGroup, Grid, IconButton, InputBase, ListItem, Stack, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { StallType } from '../../NewStores/@DomainObjects/StallType';
import MenuIcon from '@mui/icons-material/Menu';
import SearchIcon from '@mui/icons-material/Search';
import AddIcon from '@mui/icons-material/Add';
import RemoveIcon from '@mui/icons-material/Remove';

type Props = {
    value,
    onAdd,
    onSubtract
}



const InputCounter: FC<Props> = (props: Props) => {
    return (

        <Grid container>
            <Grid item>
                <IconButton sx={{ p: '10px' }} aria-label="menu">
                    <AddIcon />
                </IconButton>
            </Grid>
            <Grid item>
                <InputBase
                    sx={{ ml: 1, flex: 1 }}
                    value={0}
                    inputProps={{ 'aria-label': 'search google maps' }}
                />
            </Grid>
            <Grid item>
                <IconButton type="submit" sx={{ p: '10px' }} aria-label="search">
                    <RemoveIcon />
                </IconButton>
            </Grid>

        </Grid>

    );
}

export default InputCounter