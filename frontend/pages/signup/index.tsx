import {CircularProgress, Container } from "@mui/material";

import { NextPage } from "next";
import TopBar from "../../components/TopBar";
import { observer } from "mobx-react-lite";
import UserForm from "../../components/UserForm";

const SingupPage: NextPage = observer(() => {

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
                    <UserForm/>
                }
            </Container>
        </>
    )
})

export default SingupPage;