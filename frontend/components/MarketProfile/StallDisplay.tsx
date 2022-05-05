import CategoryIcon from '@mui/icons-material/Category';
import StorefrontIcon from '@mui/icons-material/Storefront';
import { TabContext, TabPanel } from '@mui/lab';
import { BottomNavigation, BottomNavigationAction, Box } from "@mui/material";
import { observer } from 'mobx-react-lite';
import { FC, useState } from 'react';
import { Market } from '../../NewStores/@DomainObjects/Market';
import BoothListItem from '../BoothListItem';
import StallTypeInfoList from '../StallTypeInfoList';

type Props = {
    market : Market
}

const StallDisplay: FC<Props> = (props: Props) => {
    const [value, setValue] = useState(0);

    return (
        <TabContext value={`${value}`}>
            <Box sx={{ width: '100%' }}>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <BottomNavigation
                        showLabels
                        value={value}
                        onChange={(event, newValue) => {
                            setValue(newValue);
                        }}
                    >
                        <BottomNavigationAction label="Booths" icon={<StorefrontIcon />} />
                        <BottomNavigationAction label="Available Stalls" icon={<CategoryIcon />} />
                    </BottomNavigation>
                </Box>
                <TabPanel value={"0"}>
                    {
                        props.market.booths?.map(x => <BoothListItem key={x.id} booth={x}/>)
                    }
                </TabPanel>
                <TabPanel value={"1"}>
                    {
                        <StallTypeInfoList market={props.market} />
                    }
                </TabPanel>
            </Box>
        </TabContext>
    )
}

export default observer(StallDisplay)