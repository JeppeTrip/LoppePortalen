import { Box, CircularProgress, Container, Divider, List, Paper, Typography } from "@mui/material";
import { NextPage } from "next";
import { useContext, useEffect } from "react";
import MarketListItem from "../../components/MarketListItem";
import { observer } from "mobx-react-lite";
import ErrorIcon from '@mui/icons-material/Error';
import MarketFilter from "../../components/MarketFilter";
import { StoreContext } from "../../NewStores/StoreContext";
import { flowResult } from "mobx";

const Markets: NextPage = observer(() => {
    const stores = useContext(StoreContext)

    useEffect(() => {
        flowResult(stores.marketStore.fetchAllMarkets())
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
                                    market => <> <MarketListItem key={market.id} Market={market} /> <Divider /> </>)
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
                        content()
                    }
                </Box>
            </Box>
        </Container>
    )
})

export default Markets;