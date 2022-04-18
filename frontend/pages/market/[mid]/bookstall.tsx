import { LoadingButton } from "@mui/lab";
import { Button, ButtonGroup, Container, Divider, FormControl, FormHelperText, InputLabel, List, MenuItem, Paper, Select, Stack, Typography } from "@mui/material";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import StallBookingListItem from "../../../components/StallBookingListItem";
import { Market } from "../../../NewStores/@DomainObjects/Market";
import { StoreContext } from "../../../NewStores/StoreContext";
import AddIcon from '@mui/icons-material/Add';
import { ModelState } from "../../../@types/ModelState";

type Props = {
    mid: string
}

const BookStall: NextPageAuth<Props> = observer(() => {
    const stores = useContext(StoreContext)
    const [marketId, setMarketId] = useState<string>("");
    const [selectedMarket, setSelectedMarket] = useState<Market>(null)
    const [merchantId, setMerchantId] = useState<string>("")
    const router = useRouter();

    /**
     * Comoponent will mount.
     */
    useEffect(() => {
        stores.userStore.user.fetchMerchants()
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

    const handleOnBook = () => {
        if (selectedMarket != null) {
            selectedMarket.bookStalls(parseInt(merchantId))
        }
    }

    return (
        selectedMarket != null &&
        (
            <Stack>
                <Typography variant="h2">
                    {selectedMarket.name}
                </Typography>
                <Divider />
                <Container>
                    <Paper elevation={1}>
                        <Stack spacing={2} m={1}>
                            <Typography variant="h5">
                                Book Stalls
                            </Typography>
                            <Divider />
                            <FormControl sx={{ minWidth: 120 }} fullWidth>
                                <InputLabel id="merhcant-select-label">Merchant</InputLabel>
                                <Select
                                    labelId="merhcant-select-label"
                                    id="merhcant-select"
                                    value={merchantId}
                                    label="Merchant"
                                    onChange={(event) => setMerchantId(event.target.value)}
                                >
                                    {
                                        stores.userStore.user.merchants.map(x => <MenuItem key={x.id + ""} value={x.id}>{x.name}</MenuItem>)
                                    }
                                </Select>
                                <FormHelperText>Select the merchant profile to use for the booking.</FormHelperText>
                            </FormControl>
                            <Divider />
                            <List>
                                {
                                    selectedMarket.stallTypes.map(x => <StallBookingListItem
                                        stallType={x}
                                        secondary={
                                            <ButtonGroup size="small" aria-label="booking-counter-button-group">
                                                <Button onClick={() => x.incrementBookingCount()}>+</Button>
                                                {<Button disabled>{x.bookingCount}</Button>}
                                                {<Button onClick={() => x.decrementBookingCount()}>-</Button>}
                                            </ButtonGroup>
                                        }
                                    />)
                                }
                            </List>
                            <Divider />
                            <LoadingButton
                                onClick={() => handleOnBook()}
                                loading={selectedMarket.state === ModelState.UPDATING}
                                loadingPosition="start"
                                startIcon={<AddIcon />}
                                variant="contained"
                                sx={{ mt: 3, ml: 1 }}
                            >
                                Book Stalls
                            </LoadingButton>
                            {
                                selectedMarket.state === ModelState.ERROR && <Typography variant="caption" color="red">
                                    Something went wrong.
                                    Unable to book the selected stalls.
                                </Typography>
                            }
                        </Stack>

                    </Paper>
                </Container>

            </Stack>

        )

    )
})

BookStall.requireAuth = true;


export default BookStall;