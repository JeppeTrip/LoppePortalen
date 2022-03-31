import React, { FC, useContext, useEffect, useState } from 'react';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import { Button, Checkbox, Drawer, FormControlLabel, FormGroup, ListItem, TextField, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { StoreContext } from '../../stores/StoreContext';
import { observer } from 'mobx-react-lite';
import { DateTimePicker, LocalizationProvider } from '@mui/lab';
import AdapterDateFns from '@mui/lab/AdapterDateFns';

type Props = {}

const MarketFilter: FC<Props> = (props: Props) => {
    //TODO: this window prop is not necessary, remove this from the implementation.
    const stores = useContext(StoreContext);

    return (
        <>
            {/* The implementation can be swapped with js to avoid SEO duplication of links. */}
            {/** TODO: Fix so it works on mobile. */}
            <Drawer
                variant="permanent"
                sx={{
                    zIndex: 0,
                    width: stores.uiStateStore.filterDrawerWidth,
                    flexShrink: 0,
                    [`& .MuiDrawer-paper`]: { width: stores.uiStateStore.filterDrawerWidth, boxSizing: 'border-box' },
                }}
            >
                <Toolbar />
                <Box sx={{ overflow: 'auto' }}>
                    <List>
                        <ListItem>
                            <Button 
                                variant="contained"
                                sx={{width: "100%"}}>
                                Apply Filter
                            </Button>
                        </ListItem>
                        <ListItem>
                            <FormGroup>
                                <FormControlLabel
                                    control={<Checkbox defaultChecked />}
                                    label="Cancelled Events" />
                            </FormGroup>


                        </ListItem>
                        <ListItem>
                            <LocalizationProvider dateAdapter={AdapterDateFns}>
                                <DateTimePicker
                                    renderInput={(props) => <TextField {...props} />}
                                    label="Start Date"
                                    value={new Date()}
                                    onChange={(newValue) => {
                                        console.log("on change start date")
                                    }
                                    }
                                />
                            </LocalizationProvider>
                        </ListItem>
                        <ListItem>
                            <LocalizationProvider dateAdapter={AdapterDateFns}>
                                <DateTimePicker
                                    renderInput={(props) => <TextField {...props} />}
                                    label="End Date"
                                    value={new Date()}
                                    onChange={(newValue) => {
                                        console.log("on change end date")
                                    }
                                    }
                                />
                            </LocalizationProvider>
                        </ListItem>
                    </List>
                </Box>
            </Drawer>
        </>
    )
}

export default observer(MarketFilter);