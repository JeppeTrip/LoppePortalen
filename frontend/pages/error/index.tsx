import { Container, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";

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