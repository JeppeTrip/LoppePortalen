import { Typography, Container} from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";

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