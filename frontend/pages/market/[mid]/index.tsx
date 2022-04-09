import { CircularProgress, Container, Divider, Grid, List, Paper, Typography } from "@mui/material";
import { styled } from "@mui/system";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useContext, useEffect, useState } from "react";
import TopBar from "../../../components/TopBar";
import { StoreContext } from "../../../stores/StoreContext";
import styles from './styles.module.css'
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import { useRouter } from "next/router";
import { LoadingButton } from "@mui/lab";
import CancelIcon from '@mui/icons-material/Cancel';
import StallTypeListItem from "../../../components/StallTypeListItem";
import { Market } from "../../../@types/Market";
import StallTypeInfoList from "../../../components/StallTypeInfoList";

type Props = {
    mid: string
}

const MarketProfilePageID: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const [marketId, setMarketId] = useState<string>("");
    const router = useRouter();

    /**
     * Comoponent will mount.
     * Reset the UI State to make sure that the state is at least consistent
     * when the market profile is loaded in.
     */
    useEffect(() => {
        stores.marketProfileUiStore.resetState();
    }, [])

    /**
     * Component will unmount.
     * Clean up after, to make sure the state is left in a somewhat consistent state when 
     * we are moving away from this market.
     */
    useEffect(() => {
        return () => {
            stores.marketProfileUiStore.resetState();
            stores.marketStore.selectedMarket = null;
        }
    }, [])

    /**
     * The next.js router needs to be ready to read from it.
     * When router is ready set the market id used to populate information on this page.
     */
    useEffect(() => {
        if (!router.isReady) { return };
        var { mid } = router.query
        setMarketId(mid + "")

    }, [router.isReady]);

    /**
     * When the market id has been updated search for the market through the market store.
     */
    useEffect(() => {
        if (!(marketId == "")) {
            stores.marketProfileUiStore.loadMarket();
            stores.marketStore.setSelectedMarket(parseInt(marketId))
        }
    }, [marketId])


    /**
     * When selected market is changed.
     * Update UI State.
     * If selected market is an actual market start the fetching of stalls.
     * TODO: load in the stalls with the initial load of the market, would make this 
     * more transaction safe, and would at least provide a consistent look at thet market
     * information.
     */
    useEffect(() => {
        console.log("Market success.")
        if (stores.marketStore.selectedMarket != null && stores.marketStore.selectedMarket.id > 0) {
            if (stores.marketStore.selectedMarket.id > 0) {
                stores.marketProfileUiStore.marketLoadingSuccess();
                //TODO: Set loading stalls state in the ui store.
                stores.marketProfileUiStore.loadStalls();
                stores.marketStore.fetchStallsForMarket(stores.marketStore.selectedMarket);
            }
            else {
                stores.marketProfileUiStore.hadMarketLoadingError();
            }
        }
    }, [stores.marketStore.selectedMarket])

    //When the selected market stalls have been updated change the state to show the stall information.
    useEffect(() => {
        console.log("update stalls")
        stores.marketProfileUiStore.stallLoadingSuccess()
    }, [stores.marketStore.selectedMarket])


    const handleCancel = () => {
        stores.marketStore.cancelSelectedMarket()
    }

    const profileContent = () => {
        return (
            <Grid id={"ProfileContainer"} container columns={1} spacing={1}>
                <Grid item xs={1}>
                    <Paper square={true} elevation={1}>
                        <Container maxWidth={"xl"}>
                            <Grid container columns={12}>
                                <Grid item xs={12}>
                                    <div className={styles.bannerPlaceholder} />
                                </Grid>
                                <Grid item xs={12}>
                                    <Grid container columns={12}>
                                        <Grid item xs={8}>
                                            <Grid>
                                                <Grid item xs={12}>
                                                    <Typography variant="h5">
                                                        {stores.marketStore.selectedMarket.name}
                                                    </Typography>
                                                </Grid>
                                                <Grid item xs={12}>
                                                    <Typography>
                                                        {
                                                            stores.marketStore.selectedMarket.startDate.toLocaleDateString() + " - " + stores.marketStore.selectedMarket.endDate.toLocaleDateString()
                                                        }
                                                    </Typography>
                                                </Grid>
                                            </Grid>
                                        </Grid>
                                        <Grid container
                                            item
                                            xs={4}
                                            justifyContent="flex-end"
                                            alignContent="center">
                                            <LoadingButton
                                                style={{ height: "40px" }}
                                                onClick={handleCancel}
                                                loading={stores.marketStore.isCancelling}
                                                loadingPosition="start"
                                                startIcon={<CancelIcon />}
                                                variant="outlined"
                                                disabled={stores.marketStore.selectedMarket == null || stores.marketStore.selectedMarket.isCancelled}
                                            >
                                                Cancel
                                            </LoadingButton>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </Container>
                    </Paper>
                </Grid>

                <Grid item xs={1}>
                    <Container maxWidth={"lg"}>
                        <Grid container columns={12} spacing={1}>
                            <Grid item xs={7}>
                                <Paper elevation={1}>
                                    <div className={styles.AboutInfo}>
                                        <Typography variant="h6">
                                            About
                                        </Typography>
                                        <Divider />
                                        <Typography variant="body1">
                                            {stores.marketStore.selectedMarket.description}
                                        </Typography>
                                    </div>
                                </Paper>
                            </Grid>
                            <Grid item xs={5}>
                                <Paper elevation={1}>
                                    <div className={styles.AboutInfo}>
                                        <Typography variant="h6">
                                            Stalls
                                        </Typography>
                                        <Divider />
                                        {
                                            stores.marketProfileUiStore.loadingStalls ? loadingContent() :
                                                <StallTypeInfoList market={stores.marketStore.selectedMarket} />
                                        }
                                    </div>
                                </Paper>

                            </Grid>
                        </Grid>
                    </Container>
                </Grid>
            </Grid>
        )
    }

    const loadingContent = () => {
        return (
            <Grid id={"ProfileLoadingContainer"} height="100%" container alignItems="center" justifyItems={"center"} alignContent="center" justifyContent={"center"}>
                <Grid item>
                    <CircularProgress />
                </Grid>
            </Grid>
        )
    }

    const errorContent = () => {

    }

    return (
        <>
            {
                stores.marketProfileUiStore.loadingMarket ? loadingContent() : profileContent()
            }
        </>
    )
})

export default MarketProfilePageID;