import AddIcon from '@mui/icons-material/Add';
import { Button, Grid } from "@mui/material";
import List from '@mui/material/List';
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Market } from "../../NewStores/@DomainObjects/Market";
import StallTypeInputListItem from "../StallTypeInputListItem";
import StallTypeListItem from "../StallTypeListItem";


type Props = {
    market: Market
}

const StallForm: FC<Props> = (props: Props) => {

    const handleOnClick = (event) => {
        const type = props.market.addNewStallType()
        type.select()
    }

    return (
        <Grid>
            <Grid item>
                <Button disabled={false} size="small" startIcon={<AddIcon />} onClick={handleOnClick}>
                    Add Stall
                </Button>
            </Grid>
            <Grid item xs={12}>
                <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
                    {
                        (props.market.store.rootStore.stallTypeStore.selected || props.market.store.rootStore.stallTypeStore.selected != null) && <StallTypeInputListItem stallType={props.market.store.rootStore.stallTypeStore.selected} />
                    }
                    {
                        props.market.stallTypes.length != 0 && props.market.savedStallTypes.map(type => <StallTypeListItem stallType={type} />)
                    }
                </List>
            </Grid>
        </Grid>
    );
}


export default observer(StallForm);