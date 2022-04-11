import { CircularProgress, Container } from "@mui/material";
import { observer } from "mobx-react-lite";
import MarketForm from "../../../components/MarketForm";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import { useContext, useEffect } from "react";
import { StoreContext } from "../../../stores/StoreContext";
import { useRouter } from "next/router";

const CreateMarketPage: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter();


    //Component mounts
    useEffect(() => {
        stores.marketStore.resetNewMarket();
        stores.marketFormUiStore.resetState();
        stores.stallFormUiStore.resetState();
    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {
            stores.marketStore.resetNewMarket();
            stores.marketFormUiStore.resetState();
            stores.stallFormUiStore.resetState();
        }
    }, [])

    useEffect(() => {
        if (stores.marketFormUiStore.redirect) {
            if (router.isReady && stores.marketStore.newMarket.id > 0)
                router.push(`${stores.marketStore.newMarket.id}`, undefined, { shallow: true });
        }
    }, [stores.marketFormUiStore.redirect])

    const loading = () => {
        return (
            <CircularProgress />
        )
    }

    return (
        <>
            <Container
                style={{ paddingTop: "25px" }}
                maxWidth="sm">
                {
                    <MarketForm market={stores.marketStore.newMarket}/>
                }
            </Container>
        </>
    )
})

CreateMarketPage.requireAuth = true;

export default CreateMarketPage;