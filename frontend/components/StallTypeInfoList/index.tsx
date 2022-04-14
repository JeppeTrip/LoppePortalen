import {Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";

import List from '@mui/material/List';
import StallTypeInfoListItem from "../StallTypeInfoListItem";
import { Market } from "../../@types/Market";

type Props = {
    market: Market
}



const StallTypeInfoList: FC<Props> = (props: Props) => {
    return (
        <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
            {
                props.market.stalls == null || props.market.stalls.length === 0 ? 
                <Typography variant="caption">
                    No stalls.
                </Typography> :
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