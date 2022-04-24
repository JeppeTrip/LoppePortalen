import { Typography } from "@mui/material";
import List from '@mui/material/List';
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Market } from "../../NewStores/@DomainObjects/Market";
import StallTypeInfoListItem from "../StallTypeInfoListItem";


type Props = {
    market: Market
}



const StallTypeInfoList: FC<Props> = (props: Props) => {
    return (
        <List sx={{ width: '100%'}} disablePadding>
            {
                props.market.stallTypes == null || props.market.stallTypes.length === 0 ? 
                <Typography variant="caption">
                    No stalls.
                </Typography> :
                //TODO: remember to update available once renting has been implemented.
                props.market.stallTypes.map(x => {
                    return (
                        <StallTypeInfoListItem
                            stallType={x}
                        />
                    )
                })
            }
        </List>
    );
}


export default observer(StallTypeInfoList);