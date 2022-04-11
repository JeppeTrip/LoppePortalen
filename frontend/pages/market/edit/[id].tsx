import { Button, CircularProgress, Container, Stack, Typography } from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import OrganiserForm from "../../../components/OrganiserForm";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import { useContext, useEffect, useState } from "react";
import { StoreContext } from "../../../stores/StoreContext";
import { Organiser } from "../../../@types/Organiser";
import { useRouter } from "next/router";
import { LoadingButton } from "@mui/lab";
import SaveIcon from '@mui/icons-material/Save';
import { Market } from "../../../@types/Market";
import MarketForm from "../../../components/MarketForm";

type Props = {
    id: string
}

const EditMarketPage: NextPageAuth<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const [marketId, setMarketId] = useState<string>(undefined);
    const router = useRouter();

    //mount
    useEffect(() => {
        stores.organiserStore.resetSubmitState()
    }, [])

    //Unmount
    useEffect(() => {
        return () => {
            setMarketId(undefined);
            stores.marketStore.setSelectedMarket(undefined);
            stores.marketStore.setSelectedMarket(undefined);
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
     * If you come here directly and edit organiser in the organiser store hasn't been set to anything
     * check if there is something in the current user. 
     * else go and check the backend
     * if nothing in the backend go to error page.
     */
    useEffect(() => {
        if (marketId) {
            if (!stores.marketStore.selectedMarket) {
                const market = stores.userStore.currentUser.markets.find(x => x.id === parseInt(marketId));
                if (market) {
                    stores.marketStore.setSelectedMarketObject(market);
                }
                else {
                    stores.marketStore.setSelectedMarket(parseInt(marketId))
                }
            }
        }
    }, [marketId])

    /**
     * When selected organiser has been set, create the edited organiser
     */
    useEffect(() => {
        if(stores.marketStore.selectedMarket)
        {
            const market = stores.marketStore.selectedMarket;
            stores.marketStore.setEditedMarket(
                new Market(
                    market.id, 
                    market.organiserId,
                    market.name,
                    market.startDate,
                    market.endDate,
                    market.description,
                    market.isCancelled,
                    market.stalls));
        }
    }, [stores.marketStore.selectedMarket])

    //go back when submission is done.
    useEffect(() => {
        if(stores.marketFormUiStore.submitSuccess)
        {
            router.back()
        }
    }, [stores.marketFormUiStore.submitSuccess])

    const handleSubmit = () => {
        //edit market command
    }

    const handleReset = () => {
        const market = stores.marketStore.selectedMarket;
        stores.marketStore.setEditedMarket(
            new Market(
                market.id, 
                market.organiserId,
                market.name,
                market.startDate,
                market.endDate,
                market.description,
                market.isCancelled,
                market.stalls));
    }

    return (
        <Container
            style={{ paddingTop: "25px" }}
            maxWidth="sm">
            <Stack spacing={1}>
                {
                    (stores.marketStore.editedMarket) 
                    && <MarketForm market={stores.marketStore.editedMarket} />
                }
                <LoadingButton
                    onClick={handleSubmit}
                    loading={stores.organiserStore.isSubmitting}
                    loadingPosition="start"
                    startIcon={<SaveIcon />}
                    variant="contained"

                >
                    Submit
                </LoadingButton>
                <Button
                    onClick={() => handleReset()}
                >
                    Reset
                </Button>
                {
                    //TODO: Make error handling waaay the fuck better.
                    stores.organiserStore.hadSubmissionError &&
                    <Typography variant="caption" color={"red"}>
                        Could not submit.
                    </Typography>
                }
            </Stack>
        </Container>
    )
})

EditMarketPage.requireAuth = true;

export default EditMarketPage;