import AddIcon from '@mui/icons-material/Add';
import SaveIcon from "@mui/icons-material/Save";
import { DateTimePicker, LoadingButton, LocalizationProvider, TabContext, TabList, TabPanel } from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import { Button, Container, FormControl, Grid, InputLabel, List, MenuItem, Paper, Select, Tab, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { reaction } from "mobx";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import StallListItem from "../../../components/StallListItem";
import StallTypeListItem from "../../../components/StallType";
import StallTypeInputListItem from "../../../components/StallTypeNewListItem";
import { StallType } from "../../../NewStores/@DomainObjects/StallType";
import { StoreContext } from '../../../NewStores/StoreContext';


type Props = {
    id: string
}

const EditMarketPage: NextPageAuth<Props> = observer(() => {
    const [tabValue, setTabValue] = useState('1')
    const [selectedType, setSelectedType] = useState<StallType>(null)
    const [stallDiff, setStallDiff] = useState(0)
    const stores = useContext(StoreContext);
    const [marketId, setMarketId] = useState<string>(undefined);
    const router = useRouter();

    //mount
    useEffect(() => {

    }, [])

    //Unmount
    useEffect(() => {
        return () => {

        }
    }, [])

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
        if (stores.marketStore.selectedMarket == null) {
            if (!(marketId == "")) {
                stores.marketStore.resolveSelectedMarket(parseInt(marketId))
            }
        }
    }, [marketId, stores.marketStore.selectedMarket])

    const handleAddNewStalls = () => {
        if (stallDiff > 0) {
            selectedType.saveNewStallsToMarket(stallDiff)
        }
    }

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    reaction(
        () => selectedType.totalStallCount,
        count => {
            setStallDiff(0)
        }
    )

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
                        </TabList>
                    </Box>
                    <TabPanel value="1">
                        {
                            stores.marketStore.selectedMarket != null && (
                                <>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12}>
                                            <TextField
                                                fullWidth={true}
                                                id="marketName"
                                                label="Name"
                                                variant="outlined"
                                                onChange={(event) => stores.marketStore.selectedMarket.name = event.target.value}
                                                value={stores.marketStore.selectedMarket.name} />
                                        </Grid>
                                        <Grid item xs={"auto"}>
                                            <LocalizationProvider dateAdapter={AdapterDateFns}>
                                                <DateTimePicker
                                                    renderInput={(props) => <TextField {...props} />}
                                                    label="Start Date"
                                                    value={stores.marketStore.selectedMarket.startDate}
                                                    onChange={(newValue) => {
                                                        stores.marketStore.selectedMarket.startDate = newValue;
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
                                                    value={stores.marketStore.selectedMarket.endDate}
                                                    onChange={(newValue) => {
                                                        stores.marketStore.selectedMarket.endDate = newValue;
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
                                                value={stores.marketStore.selectedMarket.description}
                                                onChange={(event) => stores.marketStore.selectedMarket.description = event.target.value}
                                                multiline
                                                rows={10}
                                            />
                                        </Grid>
                                    </Grid>
                                    <Grid>
                                        <LoadingButton
                                            onClick={() => stores.marketStore.selectedMarket.save()}
                                            loading={false}
                                            loadingPosition="start"
                                            startIcon={<SaveIcon />}
                                            variant="contained"
                                            sx={{ mt: 3, ml: 1 }}
                                        >
                                            Update
                                        </LoadingButton>
                                    </Grid>
                                </>
                            )
                        }

                    </TabPanel>
                    <TabPanel value="2">
                        {stores.marketStore.selectedMarket != null && (
                            <>
                                <Grid container spacing={2}>
                                    <Grid item>
                                        <Button disabled={stores.stallTypeStore.newStallType != null} size="small" startIcon={<AddIcon />} onClick={() => {
                                            let type = stores.marketStore.selectedMarket.addNewStallType()
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
                                                stores.marketStore.selectedMarket.stallTypes.length != 0
                                                && stores.marketStore.selectedMarket.savedStallTypes.map(type => <StallTypeListItem stallType={type} />)
                                            }
                                        </List>
                                    </Grid>
                                </Grid>
                            </>
                        )
                        }
                    </TabPanel>
                    <TabPanel value="3">
                        {stores.marketStore.selectedMarket != null && (
                            <Grid container spacing={2} alignItems="center">
                                <Grid item xs={8}>
                                    <FormControl fullWidth>
                                        <InputLabel id="stall-type-select-label">Stall Type</InputLabel>
                                        <Select
                                            labelId="stall-type-select-label"
                                            id="stall-type-select"
                                            value={selectedType == null ? "" : selectedType.id+""}
                                            label="Age"
                                            onChange={(event) => {
                                                setStallDiff(0)
                                                setSelectedType(stores.marketStore.selectedMarket.stallTypes.find(x => x.id === parseInt(event.target.value))
                                                )
                                            }}
                                        >
                                            {
                                                stores.marketStore.selectedMarket.stallTypes.map(x =>
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
                                        value={selectedType && selectedType != null ? (selectedType.totalStallCount + stallDiff) : 0}
                                        onChange={(event) => {
                                            let diff = parseInt(event.target.value) - selectedType.totalStallCount
                                            setStallDiff(diff >= 0 ? diff : Math.abs(diff) > selectedType.totalStallCount ? -selectedType.totalStallCount : diff)
                                        }
                                        }
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
                                            stores.marketStore.selectedMarket.stalls.length != 0
                                            && stores.marketStore.selectedMarket.stalls.map(stall => <StallListItem stall={stall} />)
                                        }
                                    </List>
                                </Grid>
                            </Grid>
                        )
                        }
                    </TabPanel>
                </TabContext>
            </Paper>


        </Container>

    )
})

EditMarketPage.requireAuth = true;

export default EditMarketPage;