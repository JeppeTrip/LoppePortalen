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
import { useErrorHandler } from 'react-error-boundary';
import MarketProfile from "../../../components/MarketProfile";

type Props = {
    mid: string
}

const MarketProfilePageID: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler();
    const [marketId, setMarketId] = useState<string>("");
    const [selectedMarket, setSelectedMarket] = useState<Market>(null)
    const router = useRouter();

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
                .catch(error => {
                    handleError(error);
                })
            }
        }
    }, [marketId, selectedMarket])

    const loadingContent = () => {
        return (
            <Grid id={"ProfileLoadingContainer"} height="100%" container alignItems="center" justifyItems={"center"} alignContent="center" justifyContent={"center"}>
                <Grid item>
                    <CircularProgress />
                </Grid>
            </Grid>
        )
    }
    
    return (
        <>
            {
                selectedMarket == null ? loadingContent() : <MarketProfile market={selectedMarket} />
            }
        </>
    )
})

export default MarketProfilePageID;