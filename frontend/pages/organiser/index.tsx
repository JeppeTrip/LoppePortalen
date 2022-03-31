import { Avatar, Box, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, Paper, TextField } from "@mui/material";
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
            <Paper elevation={1}>
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
            </Paper>
        )
    }
    return (
        <>
            <Container sx={{minHeight: "100%"}}>
                <Box sx={{ display: 'flex' }}>
                    <Box component="main" sx={{ flexGrow: 1, p: 0 }}>
                        {
                            stores.organiserStore.isLoading ? loading() :
                                stores.organiserStore.hadLoadingError ? error() : content()
                        }
                    </Box>
                </Box>
            </Container>

        </>
    )
})

export default OrganiserListPage;