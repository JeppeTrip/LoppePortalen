import { CircularProgress, Container, Divider, Grid, Paper, Typography } from "@mui/material";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { useErrorHandler } from "react-error-boundary";
import { Merchant } from "../../../NewStores/@DomainObjects/Merchant";
import { StoreContext } from "../../../NewStores/StoreContext";
import styles from './styles.module.css';
type Props = {
    id: string
}

const MerchantPage: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler();
    const [merchantId, setMerchantId] = useState<string>("");
    const [selectedMerchant, setSelectedMerchant] = useState<Merchant>(null)
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
        var { id } = router.query
        setMerchantId(id + "")

    }, [router.isReady]);

    /**
     * If selected market is empty in the stores search for it.
     */
    useEffect(() => {
        if (selectedMerchant == null) {
            if (!(merchantId == "")) {
                flowResult(stores.merchantStore.resolveMerchant(parseInt(merchantId)))
                    .then(res => setSelectedMerchant(res))
                    .catch(error => {
                        handleError(error)
                    })
            }
        }
    }, [merchantId, selectedMerchant])

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
                                                        {selectedMerchant.name}
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
                                    <Typography variant="h6">
                                        About
                                    </Typography>
                                    <Divider />
                                    <Typography variant="body1">
                                        {selectedMerchant.description}
                                    </Typography>
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

    return (
        <>
            {
                selectedMerchant == null ? loadingContent() : profileContent()
            }
        </>
    )
})

export default MerchantPage;