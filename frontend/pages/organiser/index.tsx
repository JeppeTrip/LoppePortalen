import {Typography, Box, CircularProgress, Container, List, Paper} from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import { useContext, useEffect } from "react";
import { StoreContext } from '../../stores/StoreContext'
import OrganiserListItem from "../../components/OrganiserListItem";
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
                {
                    stores.organiserStore.organisers.length == 0 ?
                        <Typography variant="subtitle2">
                            No organisers found.
                        </Typography>
                        :
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
                }

            </Paper>
        )
    }
    return (
        <>
            <Container>
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