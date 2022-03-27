import { Avatar, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, TextField } from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import styles from './styles.module.css'
import { RootStore } from "../../stores/RootStore";
import { useContext } from "react";
import { StoreContext } from '../../stores/StoreContext'
import OrganiserListItem from "../../components/OrganiserListItem";
import TopBar from "../../components/TopBar";

const OrganiserListPage: NextPage = observer(() => {
    const stores = useContext(StoreContext);

    const loading = () => {
        return (
                <CircularProgress />
        )
    }
     
    const content = () => {
        return (
            <List>
                {
                    stores.organiserStore.organisers.map(organiser =>
                        <>
                            {
                                <OrganiserListItem Organiser={organiser} />
                            }
                        </>)
                }
            </List>
        )
    }
    return (
        <>
            <Container                 
                className={stores.organiserStore.isLoading ? 
                styles.ContainerLoading : styles.Container} 
                maxWidth="sm">
                    <TopBar />
                    {
                    stores.organiserStore.isLoading ? loading() : content()
                    }
            </Container>
        </>
    )
})

export default OrganiserListPage;