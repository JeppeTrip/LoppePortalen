import { CircularProgress, Container, Divider, Grid, Paper, Typography } from "@mui/material";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { useErrorHandler } from "react-error-boundary";
import BoothProfile from "../../../components/BoothProfile";
import { Booth } from "../../../NewStores/@DomainObjects/Booth";
import { StoreContext } from "../../../NewStores/StoreContext";
import styles from './styles.module.css';

type Props = {
    id: string
}

const BoothProfilePage: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler();
    const [boothId, setBoothId] = useState<string>("");
    const [selectedBooth, setSelectedBooth] = useState<Booth>(null)
    const router = useRouter();

    /**
     * The next.js router needs to be ready to read from it.
     * When router is ready set the market id used to populate information on this page.
     */
    useEffect(() => {
        if (!router.isReady) { return };
        var { id } = router.query
        setBoothId(id + "")

    }, [router.isReady]);

    /**
     * If selected market is empty in the stores search for it.
     */
    useEffect(() => {
        if (selectedBooth == null) {
            if (!(boothId == "")) {
                flowResult(stores.boothStore.fetchBooth(boothId))
                    .then(res => {
                        setSelectedBooth(res)
                    }).catch(error => {
                        handleError(error)
                    })
            }
        }
    }, [boothId, selectedBooth])

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
                selectedBooth == null ? loadingContent() : <BoothProfile booth={selectedBooth} />
            }
        </>
    )
})

export default BoothProfilePage;