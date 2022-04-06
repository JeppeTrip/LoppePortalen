import { Typography, Box, CircularProgress, Container, List, Paper } from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import { useContext, useEffect } from "react";
import { StoreContext } from '../../stores/StoreContext'
import OrganiserListItem from "../../components/OrganiserListItem";
import ErrorIcon from '@mui/icons-material/Error';

const ErrorPage: NextPage = observer(() => {
    return (
        <>
            <Container sx={{padding: 24}}>
                <Typography variant="h2">
                    You have taken a wrong turn.
                </Typography>
                <Typography>
                    We couldn't find the page you are looking for. Likely still under development.
                </Typography>
            </Container>

        </>
    )
})

export default ErrorPage;