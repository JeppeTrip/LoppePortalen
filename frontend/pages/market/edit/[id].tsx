import AddIcon from '@mui/icons-material/Add';
import SaveIcon from "@mui/icons-material/Save";
import { DateTimePicker, LoadingButton, LocalizationProvider, TabContext, TabList, TabPanel } from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import { Button, Container, FormControl, Grid, InputLabel, List, MenuItem, Paper, Select, Tab, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { flowResult, reaction } from "mobx";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useCallback, useContext, useEffect, useState } from "react";
import { useErrorHandler } from 'react-error-boundary';
import { ModelState } from '../../../@types/ModelState';
import { NextPageAuth } from "../../../@types/NextAuthPage";
import RegionInput from '../../../components/RegionInput';
import StallListItem from "../../../components/StallListItem";
import StallTypeListItem from "../../../components/StallType";
import StallTypeInputListItem from "../../../components/StallTypeNewListItem";
import { Market } from '../../../NewStores/@DomainObjects/Market';
import { StallType } from "../../../NewStores/@DomainObjects/StallType";
import { StoreContext } from '../../../NewStores/StoreContext';


type Props = {
    id: string
}

const EditMarketPage: NextPageAuth<Props> = observer(() => {
    const handleError = useErrorHandler()
    const [tabValue, setTabValue] = useState('1')
    const [selectedType, setSelectedType] = useState<StallType>(null)
    const [stallToAdd, setStallsToAdd] = useState(0)
    const [selectedMarket, setSelectedMarket] = useState<Market>(null)
    const stores = useContext(StoreContext);
    const [marketId, setMarketId] = useState<string>(undefined);
    const router = useRouter();

    const [file, setFile] = useState<File>()

    const saveFile = (e) => {
        setFile(e.target.files[0])
    }

    const uploadFile = useCallback(() => {
        selectedMarket.uploadBanner(file)
    }, [selectedMarket, file])

    useEffect(() => {
        if (!router.isReady) {
            return
        };
        var { id } = router.query
        setMarketId(id + "")
    }, [router.isReady]);

    /**
     * If selected market is empty in the stores search for it.
     */
    useEffect(() => {
        if (selectedMarket == null) {
            if (marketId && marketId != "") {
                flowResult(stores.marketStore.fetchMarket(parseInt(marketId)))
                    .then(res => {
                        setSelectedMarket(res)
                    }).catch(error => {
                        handleError(error)
                    });
            }
        }
    }, [marketId, selectedMarket])

    const handleAddNewStalls = useCallback(() => {
        if (stallToAdd > 0) {
            selectedType.saveNewStallsToMarket(stallToAdd)
        }
    }, [stallToAdd])

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    const handleOnRegionChange = useCallback((postalCode : string, city : string) => {
        console.log("psotal")
        console.log(postalCode)
        selectedMarket.postalCode = postalCode
        selectedMarket.city = city
    }, [selectedMarket, selectedMarket?.city, selectedMarket?.postalCode])

    return (
        <Container >
            <Paper elevation={1}>
                <Typography variant="h2">
                    Edit Market
                </Typography>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="lab API tabs example">
                            <Tab label="Market Info" value="1" />
                            <Tab label="Stall Types" value="2" />
                            <Tab label="Stalls" value="3" />
                            <Tab label="Images" value="4" />
                        </TabList>
                    </Box>
                    <TabPanel value="1">
                        {
                            selectedMarket != null && (
                                <>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12}>
                                            <TextField
                                                fullWidth={true}
                                                id="marketName"
                                                label="Name"
                                                variant="outlined"
                                                onChange={(event) => selectedMarket.name = event.target.value}
                                                value={selectedMarket.name} />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextField
                                                fullWidth={true}
                                                id="marketAddress"
                                                label="Address"
                                                variant="outlined"
                                                onChange={(event) => selectedMarket.address = event.target.value}
                                                value={selectedMarket.address} />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <RegionInput postalCode={selectedMarket.postalCode} city={selectedMarket.city} onChange={handleOnRegionChange} />
                                        </Grid>
                                        <Grid item xs={"auto"}>
                                            <LocalizationProvider dateAdapter={AdapterDateFns}>
                                                <DateTimePicker
                                                    renderInput={(props) => <TextField {...props} />}
                                                    label="Start Date"
                                                    value={selectedMarket.startDate}
                                                    onChange={(newValue) => {
                                                        selectedMarket.startDate = newValue;
                                                    }
                                                    }
                                                />
                                            </LocalizationProvider>
                                        </Grid>
                                        <Grid item xs={"auto"}>
                                            <LocalizationProvider dateAdapter={AdapterDateFns}>
                                                <DateTimePicker
                                                    renderInput={(props) => <TextField {...props} />}
                                                    label="End Date"
                                                    value={selectedMarket.endDate}
                                                    onChange={(newValue) => {
                                                        selectedMarket.endDate = newValue;
                                                    }
                                                    }
                                                />
                                            </LocalizationProvider>
                                        </Grid>

                                        <Grid item xs={12}>
                                            <TextField
                                                fullWidth
                                                id="outlined-multiline-static"
                                                label="Description"
                                                value={selectedMarket.description}
                                                onChange={(event) => selectedMarket.description = event.target.value}
                                                multiline
                                                rows={10}
                                            />
                                        </Grid>
                                    </Grid>
                                    <Grid>
                                        <LoadingButton
                                            onClick={() => selectedMarket.save()}
                                            loading={selectedMarket.state === ModelState.SAVING}
                                            loadingPosition="start"
                                            startIcon={<SaveIcon />}
                                            variant="contained"
                                            sx={{ mt: 3, ml: 1 }}
                                        >
                                            Update
                                        </LoadingButton>
                                    </Grid>
                                    {
                                        selectedMarket.state === ModelState.ERROR &&
                                        (
                                            <Grid>
                                                <Grid item>
                                                    <Typography variant="caption" color="red">
                                                        Something went wrong.
                                                        Unable to submit new market.
                                                    </Typography>
                                                </Grid>
                                            </Grid>
                                        )
                                    }
                                </>
                            )
                        }

                    </TabPanel>
                    <TabPanel value="2">
                        {selectedMarket != null && (
                            <>
                                <Grid container spacing={2}>
                                    <Grid item>
                                        <Button disabled={stores.stallTypeStore.newStallType != null} size="small" startIcon={<AddIcon />} onClick={() => {
                                            let type = selectedMarket.addNewStallType()
                                        }}>
                                            Add Stall
                                        </Button>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
                                            {
                                                (stores.stallTypeStore.newStallType
                                                    || stores.stallTypeStore.newStallType != null)
                                                && <StallTypeInputListItem stallType={stores.stallTypeStore.newStallType} />
                                            }
                                            {
                                                selectedMarket.stallTypes.length != 0
                                                && selectedMarket.savedStallTypes.map(type => <StallTypeListItem stallType={type} />)
                                            }
                                        </List>
                                    </Grid>
                                </Grid>
                            </>
                        )
                        }
                    </TabPanel>
                    <TabPanel value="3">
                        {selectedMarket != null && (
                            <Grid container spacing={2} alignItems="center">
                                <Grid item xs={8}>
                                    <FormControl fullWidth>
                                        <InputLabel id="stall-type-select-label">Stall Type</InputLabel>
                                        <Select
                                            labelId="stall-type-select-label"
                                            id="stall-type-select"
                                            value={selectedType == null ? "" : selectedType.id + ""}
                                            label="Age"
                                            onChange={(event) => {
                                                setStallsToAdd(0)
                                                setSelectedType(selectedMarket.stallTypes.find(x => x.id === parseInt(event.target.value))
                                                )
                                            }}
                                        >
                                            {
                                                selectedMarket.stallTypes.map(x =>
                                                    <MenuItem value={x.id}>{x.name}</MenuItem>
                                                )
                                            }
                                        </Select>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={2}>
                                    <TextField
                                        fullWidth={true}
                                        type="number"
                                        value={stallToAdd}
                                        onChange={(event) => {
                                            setStallsToAdd(Math.max(parseInt(event.target.value), 0))
                                        }}
                                    />
                                </Grid>
                                <Grid item>
                                    <LoadingButton
                                        onClick={() => handleAddNewStalls()}
                                        loading={false}
                                        loadingPosition="start"
                                        startIcon={<SaveIcon />}
                                        variant="contained"
                                    >
                                        Add Stalls
                                    </LoadingButton>
                                </Grid>
                                <Grid item xs={12}>
                                    <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
                                        {
                                            selectedMarket.stalls.length != 0
                                            && selectedMarket.stalls.map(stall => <StallListItem stall={stall} />)
                                        }
                                    </List>
                                </Grid>
                            </Grid>
                        )
                        }
                    </TabPanel>
                    <TabPanel value="4">
                        {
                            (selectedMarket != null) &&
                            <>
                                <TextField
                                    id="outlined-full-width"
                                    label="Image Upload"
                                    name="upload-photo"
                                    type="file"
                                    fullWidth
                                    margin="normal"
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    variant="outlined"
                                    onChange={saveFile}
                                />
                                <Button onClick={uploadFile}> Upload Banner</Button>
                            </>
                        }
                    </TabPanel>
                </TabContext>
            </Paper>


        </Container>

    )
})

EditMarketPage.requireAuth = true;

export default EditMarketPage;