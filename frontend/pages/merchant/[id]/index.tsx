import { CircularProgress, Grid } from "@mui/material";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { useErrorHandler } from "react-error-boundary";
import MerchantProfile from "../../../components/MerchantProfile";
import { Merchant } from "../../../NewStores/@DomainObjects/Merchant";
import { StoreContext } from "../../../NewStores/StoreContext";

type Props = {
    id: string
}

const MerchantProfilePage: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler();
    const [merchantId, setMerchantId] = useState<string>("");
    const [selectedMerchant, setSelectedMerchant] = useState<Merchant>(null)
    const router = useRouter();

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
                flowResult(stores.merchantStore.fetchMerchant(parseInt(merchantId)))
                    .then(res => setSelectedMerchant(res))
                    .catch(error => {
                        handleError(error)
                    })
            }
        }
    }, [merchantId, selectedMerchant])

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
                selectedMerchant == null ? loadingContent() : <MerchantProfile merchant={selectedMerchant} />
            }
        </>
    )
})

export default MerchantProfilePage;