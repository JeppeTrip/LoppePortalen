import { CircularProgress, Container } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect } from "react";
import { ModelState } from "../../../@types/ModelState";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import MarketForm from "../../../components/MarketForm";
import { StoreContext } from "../../../NewStores/StoreContext";

const CreateMarketPage: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    //Component mounts
    useEffect(() => {

    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {
            if(stores.marketStore.selectedMarket?.state != ModelState.IDLE)
            {
                stores.marketStore.selectedMarket.deselect()
                stores.marketStore.removeUnsavedMarketsFromList()
            }
        }
    }, [])

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
                    <MarketForm market={stores.marketStore.createMarket()} title={"Create Market"}/>
                }
            </Container>
        </>
    )
})

CreateMarketPage.requireAuth = true;

export default CreateMarketPage;