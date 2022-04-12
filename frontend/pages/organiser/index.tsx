import {Typography, Box, CircularProgress, Container, List, Paper} from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import { useContext, useEffect } from "react";
import OrganiserListItem from "../../components/OrganiserListItem";
import ErrorIcon from '@mui/icons-material/Error';
import { StoreContext } from "../../NewStores/StoreContext";
import { flowResult } from "mobx";

const OrganiserListPage: NextPage = observer(() => {
    const stores = useContext(StoreContext);

    useEffect(() => {
        stores.organiserStore.resolveOrganisersAll()
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
                            //put loading and error content back here.
                            content()
                        }
                    </Box>
                </Box>
            </Container>

        </>
    )
})

export default OrganiserListPage;