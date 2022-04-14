import SaveIcon from '@mui/icons-material/Save';
import { LoadingButton } from "@mui/lab";
import { Button, Container, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import MarketForm from "../../../components/MarketForm";
import { StoreContext } from '../../../NewStores/StoreContext';


type Props = {
    id: string
}

const EditMarketPage: NextPageAuth<Props> = observer(() => {
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

    return (
        <Container
            style={{ paddingTop: "25px" }}
            maxWidth="sm">
            {
                (stores.marketStore.selectedMarket)
                && <MarketForm market={stores.marketStore.selectedMarket} title={"Edit Market"} />
            }
        </Container>
    )
})

EditMarketPage.requireAuth = true;

export default EditMarketPage;