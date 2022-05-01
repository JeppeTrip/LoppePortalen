import { DateTimePicker, LocalizationProvider } from '@mui/lab';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import { Autocomplete, Button, Drawer, Stack, TextField } from '@mui/material';
import Toolbar from '@mui/material/Toolbar';
import { Box } from '@mui/system';
import { observer } from 'mobx-react-lite';
import React, { FC, useContext, useEffect, useState } from 'react';
import { StoreContext } from '../../NewStores/StoreContext';

const drawerWidth = 300

type Props = {}

const BoothFilter: FC<Props> = (props: Props) => {
    //TODO: this window prop is not necessary, remove this from the implementation.
    const stores = useContext(StoreContext);
    const [startDate, setStartDate] = useState(null)
    const [endDate, setEndDate] = useState(null)
    const [categories, setCategories] = useState<string[]>([])

    useEffect(() => {
        stores.itemCategoryStore.fetchCategories()
    }, [])

    const handleSubmit = (event) => {
        //Todo: expand with organiser filter
        stores.boothStore.fetchFilteredBooths(startDate, endDate, categories)
    }

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

export default observer(BoothFilter);