import {CircularProgress, Container } from "@mui/material";

import { NextPage } from "next";
import TopBar from "../../../components/TopBar";
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
                <TopBar />
                {
                    <MarketForm/>
                }
            </Container>
        </>
    )
})

export default CreateMarketPage;