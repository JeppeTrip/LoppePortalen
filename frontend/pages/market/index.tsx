import { Avatar, CircularProgress, Container, List, ListItem, ListItemAvatar, ListItemText } from "@mui/material";
import ImageIcon from '@mui/icons-material/Image'
import WorkIcon from '@mui/icons-material/Work'
import BeachAccessIcon from '@mui/icons-material/BeachAccess'
import { NextPage } from "next";
import TopBar from "../../components/TopBar";
import styles from './styles.module.css'
import { useContext, useEffect } from "react";
import { StoreContext } from "../../stores/StoreContext";

const Markets: NextPage = () => {
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
                <ListItem>
                    <ListItemAvatar>
                        <Avatar>
                            <ImageIcon />
                        </Avatar>
                    </ListItemAvatar>
                    <ListItemText primary="Photos" secondary="Jan 9, 2014" />
                </ListItem>
                <ListItem>
                    <ListItemAvatar>
                        <Avatar>
                            <WorkIcon />
                        </Avatar>
                    </ListItemAvatar>
                    <ListItemText primary="Work" secondary="Jan 7, 2014" />
                </ListItem>
                <ListItem>
                    <ListItemAvatar>
                        <Avatar>
                            <BeachAccessIcon />
                        </Avatar>
                    </ListItemAvatar>
                    <ListItemText primary="Vacation" secondary="July 20, 2014" />
                </ListItem>
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
}

export default Markets;