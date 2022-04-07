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
    const [selectedMarket, setSelectedMarket] = useState(null)
    const [marketId, setMarketId] = useState<string>("");
    const router = useRouter();

    useEffect(() => {
        if (!router.isReady) { return };
        var { mid } = router.query
        setMarketId(mid + "")

    }, [router.isReady]);

    useEffect(() => {
        if (stores.marketStore.selectedMarket == null) {
            if (!(marketId == "")) {
                stores.marketStore.setSelectedMarket(parseInt(marketId));
            }
        }
    }, [marketId])

    useEffect(() => {
        if (stores.marketStore.selectedMarket != null) {
            setSelectedMarket(stores.marketStore.selectedMarket)
        }
    }, [stores.marketStore.selectedMarket])

    //Adding a return statement is the same as componentWillUnMount - that is pretty damn obscure.
    useEffect(() => {
        return () => {
            stores.marketStore.selectedMarket = null;
        }
    }, [])

    const handleCancel = () => {
        stores.marketStore.cancelSelectedMarket()
    }

    return (
        <>
            {
                selectedMarket == null ? <CircularProgress /> :
                    <Grid container columns={1} spacing={1}>
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
                                                                {selectedMarket.name}
                                                            </Typography>
                                                        </Grid>
                                                        <Grid item xs={12}>
                                                            <Typography>
                                                                {
                                                                    selectedMarket.startDate.toLocaleDateString() + " - " + selectedMarket.endDate.toLocaleDateString()
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
                                                    {selectedMarket.description}
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
                                                    <StallTypeInfoList market={stores.marketStore.selectedMarket} />
                                                }
                                            </div>
                                        </Paper>

                                    </Grid>
                                </Grid>
                            </Container>
                        </Grid>
                    </Grid>
            }


        </>
    )
})

export default MarketProfilePageID;