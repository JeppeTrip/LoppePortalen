import {Box, CircularProgress, Container, Divider, List, Paper, Typography } from "@mui/material";
import { NextPage } from "next";
import { useContext, useEffect } from "react";
import { StoreContext } from "../../stores/StoreContext";
import MarketListItem from "../../components/MarketListItem";
import { observer } from "mobx-react-lite";
import ErrorIcon from '@mui/icons-material/Error';
import MarketFilter from "../../components/MarketFilter";

const Markets: NextPage = observer(() => {
    const stores = useContext(StoreContext);

    useEffect(() => {
        stores.marketStore.loadMarkets()
    }, [])

    const error = () => {
        return (
            <ErrorIcon sx={{ fontSize: 50 }} />
        )
    }

    const loading = () => {
        return (
            <CircularProgress />
        )
    }

    const content = () => {
        return (
            <Paper elevation={1}>
                {
                    stores.marketStore.markets.length == 0 ?
                        <Typography variant="subtitle2">
                            No markets found.
                        </Typography>
                        :
                        <List>
                            {
                                stores.marketStore.markets.map(
                                    market => <> <MarketListItem Market={market}/> <Divider /> </>)
                            }
                        </List>
                }

            </Paper>
        );
    }

    return (
        <Container>
            <Box sx={{ display: 'flex' }}>
                <MarketFilter />
                <Box component="main" sx={{ flexGrow: 1, p: 0 }}>
                    {
                        stores.marketStore.isLoading ? loading() :
                            stores.marketStore.hadLoadingError ? error() : content()}
                </Box>
            </Box>
        </Container>
    )
})

export default Markets;