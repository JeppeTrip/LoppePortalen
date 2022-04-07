import { Button, Grid, IconButton, Input, ListItem, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";

import List from '@mui/material/List';
import { StoreContext } from "../../stores/StoreContext";
import StallTypeInfoListItem from "../StallTypeInfoListItem";
import { IMarket } from "../../@types/Market";

type Props = {
    market: IMarket
}



const StallTypeInfoList: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);

    return (
        <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
            {
                //TODO: remember to update available once renting has been implemented.
                props.market.uniqueStalls().map(x => {
                    return (
                        <StallTypeInfoListItem
                            stall={x}
                            available={props.market.stallCount(x.type)}
                            total={props.market.stallCount(x.type)}
                        />
                    )
                })
            }
        </List>
    );
}


export default observer(StallTypeInfoList);