import { CircularProgress, Container, Divider, Grid, Paper, Typography } from "@mui/material";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import StallDisplay from "../../../components/MarketProfile/StallDisplay";
import { Market } from "../../../NewStores/@DomainObjects/Market";
import { StoreContext } from "../../../NewStores/StoreContext";
import styles from './styles.module.css';

type Props = {
    mid: string
}

const MarketProfilePageID: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const [marketId, setMarketId] = useState<string>("");
    const [selectedMarket, setSelectedMarket] = useState<Market>(null)
    const router = useRouter();

    /**
     * Comoponent will mount.
     */
    useEffect(() => {

    }, [])

    /**
     * Component will unmount.
     */
    useEffect(() => {
        return () => {

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
     * If selected market is empty in the stores search for it.
     */
    useEffect(() => {
        if (selectedMarket == null) {
            if (!(marketId == "")) {
                flowResult(stores.marketStore.fetchMarket(parseInt(marketId)))
                .then(res => {
                    setSelectedMarket(res)
                })
            }
        }
    }, [marketId, selectedMarket])

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
                                    <StallDisplay market={selectedMarket}/>
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
                selectedMarket == null ? loadingContent() : profileContent()
            }
        </>
    )
})

export default MarketProfilePageID;