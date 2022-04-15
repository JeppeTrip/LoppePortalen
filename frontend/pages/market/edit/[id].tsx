import { DateTimePicker, LoadingButton, LocalizationProvider, TabContext, TabList, TabPanel } from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import { Button, Container, Grid, List, Paper, Tab, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import { StoreContext } from '../../../NewStores/StoreContext';
import SaveIcon from "@mui/icons-material/Save"
import { Box } from "@mui/system";
import AddIcon from '@mui/icons-material/Add';
import StallTypeInputListItem from "../../../components/StallTypeNewListItem";
import StallTypeListItem from "../../../components/StallType";


type Props = {
    id: string
}

const EditMarketPage: NextPageAuth<Props> = observer(() => {
    const [tabValue, setTabValue] = useState('1')
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

    const handleSubmit = () => {

    }

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

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
                    <TabPanel value="3">Functions to edit stalls here.</TabPanel>
                </TabContext>
            </Paper>


        </Container>

    )
})

EditMarketPage.requireAuth = true;

export default EditMarketPage;