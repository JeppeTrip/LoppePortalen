import { Avatar, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, TextField } from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import styles from './styles.module.css'
import { RootStore } from "../../stores/RootStore";
import { useContext, useEffect } from "react";
import { StoreContext } from '../../stores/StoreContext'
import OrganiserListItem from "../../components/OrganiserListItem";
import TopBar from "../../components/TopBar";
import ErrorIcon from '@mui/icons-material/Error';

const OrganiserListPage: NextPage = observer(() => {
    const stores = useContext(StoreContext);

    useEffect(() => {
        stores.organiserStore.loadOrganisers()
    }, [])

    const loading = () => {
        return (
            <CircularProgress />
        )
    }

    const error = () => {
        return (
            <ErrorIcon sx={{ fontSize: 50 }} />
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
                className={stores.organiserStore.isLoading || stores.organiserStore.hadLoadingError ?
                    styles.ContainerLoading : styles.Container}
                maxWidth="sm">
                <TopBar />
                {
                    stores.organiserStore.isLoading ? loading() :
                        stores.organiserStore.hadLoadingError ? error() : content()
                }
            </Container>
        </>
    )
})

export default OrganiserListPage;