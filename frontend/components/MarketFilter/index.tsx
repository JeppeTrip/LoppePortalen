import { DateTimePicker, LocalizationProvider } from '@mui/lab';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import { Autocomplete, Button, Checkbox, Drawer, FormControlLabel, FormGroup, InputAdornment, Stack, TextField } from '@mui/material';
import Toolbar from '@mui/material/Toolbar';
import { Box } from '@mui/system';
import { observer } from 'mobx-react-lite';
import React, { FC, useCallback, useContext, useEffect, useState } from 'react';
import { Location } from '../../@types/Location';
import { StoreContext } from '../../NewStores/StoreContext';
import RegionInput from '../RegionInput';

const drawerWidth = 300

type Props = {}

const MarketFilter: FC<Props> = (props: Props) => {
    //TODO: this window prop is not necessary, remove this from the implementation.
    const stores = useContext(StoreContext);
    const [hideCancelledEvents, setHideCancelledEvents] = useState(true);
    const [startDate, setStartDate] = useState(null)
    const [endDate, setEndDate] = useState(null)
    const [categories, setCategories] = useState<string[]>([])
    const [location, setLocation] = useState<Location>(null)
    const [distance, setDistance] = useState(10)

    useEffect(() => {
        stores.itemCategoryStore.fetchCategories()
    }, [])

    const toggleHideCancelledEvents = (event) => {
        setHideCancelledEvents(!hideCancelledEvents)
    }

    const handleSubmit = (event) => {
        //Todo: expand with organiser filter
        stores.marketStore.fetchFilteredMarkets(null, hideCancelledEvents, startDate, endDate, categories, location?.x, location?.y, distance * 1000) //km conversion
    }

    const handleOnLocationChange = useCallback((location : Location) => {
        setLocation(location)
    }, [location, setLocation])

    return (
        <>
            {/* The implementation can be swapped with js to avoid SEO duplication of links. */}
            {/** TODO: Fix so it works on mobile. */}
            <Drawer
                variant="permanent"
                sx={{
                    zIndex: 1,
                    width: drawerWidth,
                    flexShrink: 1,
                    [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
                }}
            >
                <Toolbar />
                <Box sx={{ overflow: 'auto' }}>
                    <Stack spacing={2} sx={{ p: 2 }}>
                        <Button
                            variant="contained"
                            sx={{ width: "100%" }}
                            onClick={handleSubmit} >
                            Apply Filter
                        </Button>

                        <FormGroup>
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={hideCancelledEvents}
                                        onChange={toggleHideCancelledEvents}
                                    />
                                }
                                label="Hide Cancelled Events" />
                        </FormGroup>

                        <LocalizationProvider dateAdapter={AdapterDateFns}>
                            <DateTimePicker
                                renderInput={(props) => <TextField {...props} />}
                                label="Start Date"
                                value={startDate}
                                onChange={(newValue) => {
                                    setStartDate(newValue)
                                }
                                }
                            />
                        </LocalizationProvider>
                        <LocalizationProvider dateAdapter={AdapterDateFns}>
                            <DateTimePicker
                                renderInput={(props) => <TextField {...props} />}
                                label="End Date"
                                value={endDate}
                                onChange={(newValue) => {
                                    setEndDate(newValue)
                                }
                                }
                            />
                        </LocalizationProvider>
                        
                        <RegionInput value={location} onChange={handleOnLocationChange} />
                        <TextField 
                            disabled
                            value={location ? location.x : ""}
                        />
                        <TextField 
                            disabled
                            value={location ? location.y : ""}
                        />
                        <TextField
                            id="distanceInput"
                            label="Distance"
                            type="number"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value = {distance}
                            onChange = {(event) => setDistance(parseFloat(event.target.value) < 0 ? 0 : parseFloat(event.target.value))}
                            InputProps={{
                                endAdornment: <InputAdornment position="start">km</InputAdornment>,
                              }}
                        />
                        <Autocomplete
                            onChange={(event, value) => setCategories(value)}
                            fullWidth
                            multiple
                            id="tags-standard"
                            value={categories}
                            loading={stores.itemCategoryStore.categories.length === 0}
                            loadingText={"Fetching categories..."}
                            options={
                                stores.itemCategoryStore.categories
                            }
                            getOptionLabel={(option) => option}
                            renderInput={(params) => (
                                <TextField
                                    {...params}
                                    variant="outlined"
                                    label="Item Categories"
                                    placeholder="Item Categories"
                                />
                            )}
                        />
                    </Stack>
                </Box>
            </Drawer>
        </>
    )
}

export default observer(MarketFilter);