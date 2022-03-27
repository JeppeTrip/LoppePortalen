import { Avatar, CircularProgress, Container, Divider, List, ListItem, ListItemAvatar, ListItemText } from "@mui/material";
import ImageIcon from '@mui/icons-material/Image'
import WorkIcon from '@mui/icons-material/Work'
import BeachAccessIcon from '@mui/icons-material/BeachAccess'
import { NextPage } from "next";
import TopBar from "../../components/TopBar";
import styles from './styles.module.css'
import { useContext, useEffect } from "react";
import { StoreContext } from "../../stores/StoreContext";
import MarketListItem from "../../components/MarketListItem";
import { observer } from "mobx-react-lite";

const Markets: NextPage = observer(() => {
    const stores = useContext(StoreContext);

    useEffect(() => {
        console.log(stores)
    }, [stores])

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
                        market => <> <MarketListItem Market={market}/> <Divider /> </>)
                }
            </List>
        );
    }

    return (
        <>
            <Container 
                className={stores.marketStore.isLoading ? 
                    styles.ContainerLoading : styles.Container} 
                maxWidth="sm">
            <TopBar />
                {
                    stores.marketStore.isLoading ? loading() : content()
                }
            </Container>
        </>
    )
})

export default Markets;