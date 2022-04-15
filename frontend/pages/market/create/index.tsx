import { CircularProgress, Container } from "@mui/material";
import { observer } from "mobx-react-lite";
import MarketForm from "../../../components/MarketForm";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import { useContext, useEffect, useLayoutEffect } from "react";
import { useRouter } from "next/router";
import { StoreContext } from "../../../NewStores/StoreContext";
import { ModelState } from "../../../@types/ModelState";

const CreateMarketPage: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    //Component mounts
    useEffect(() => {

    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {

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