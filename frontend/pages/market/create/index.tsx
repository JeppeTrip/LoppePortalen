import {CircularProgress, Container } from "@mui/material";

import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import MarketForm from "../../../components/MarketForm";
import { NextPageAuth } from "../../../@types/NextAuthPage";

const CreateMarketPage: NextPageAuth = observer(() => {

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

CreateMarketPage.requireAuth = true;

export default CreateMarketPage;