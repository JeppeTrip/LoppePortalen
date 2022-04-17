import ErrorIcon from '@mui/icons-material/Error';
import { Box, CircularProgress, Container, Divider, List, Paper, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useContext, useEffect } from "react";
import MerchantListItem from '../../components/MerchantListItem';
import { StoreContext } from "../../NewStores/StoreContext";


const Merchants: NextPage = observer(() => {
    const stores = useContext(StoreContext)

    useEffect(() => {
        stores.merchantStore.getAllMerchants()
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
                    stores.merchantStore.merchants.length == 0 ?
                        <Typography variant="subtitle2">
                            No merchants found.
                        </Typography>
                        :
                        <List>
                            {
                                stores.merchantStore.merchants.map(
                                    x => <> <MerchantListItem merchant={x}/><Divider /> </>)
                            }
                        </List>
                }

            </Paper>
        );
    }

    return (
        <Container>
            <Box sx={{ display: 'flex' }}>
                <Box component="main" sx={{ flexGrow: 1, p: 0 }}>
                    {
                        content()
                    }
                </Box>
            </Box>
        </Container>
    )
})

export default Merchants;