import { Avatar, CircularProgress, Container, Divider, List, ListItem, ListItemAvatar, ListItemText } from "@mui/material";
import { NextPage } from "next";
import styles from './styles.module.css'
import { useContext, useEffect } from "react";
import { StoreContext } from "../../stores/StoreContext";
import MarketListItem from "../../components/MarketListItem";
import { observer } from "mobx-react-lite";
import ErrorIcon from '@mui/icons-material/Error';

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
            <List>
                {
                    stores.marketStore.markets.map(
                        market => <> <MarketListItem Market={market} /> <Divider /> </>)
                }
            </List>
        );
    }

    return (
        <>
            <Container
                className={stores.marketStore.isLoading || stores.marketStore.hadLoadingError ?
                    styles.ContainerLoading : styles.Container}
                maxWidth="xl">
                {
                    stores.marketStore.isLoading ? loading() : 
                    stores.marketStore.hadLoadingError ? error() : content()
                }
            </Container>
        </>
    )
})

export default Markets;