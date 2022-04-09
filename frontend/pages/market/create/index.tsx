import {CircularProgress, Container } from "@mui/material";

import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import MarketForm from "../../../components/MarketForm";

const CreateMarketPage: NextPage = observer(() => {

    const loading = () => {
        return (
            <CircularProgress />
        )
    }

    return (
        <>
            <Container
                style={{ paddingTop: "25px" }}
                maxWidth="sm">
                {
                    <MarketForm/>
                }
            </Container>
        </>
    )
})

export default CreateMarketPage;