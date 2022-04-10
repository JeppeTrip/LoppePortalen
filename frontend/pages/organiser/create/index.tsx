import {CircularProgress, Container} from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import OrganiserForm from "../../../components/OrganiserForm";
import { NextPageAuth } from "../../../@types/NextAuthPage";

const CreateOrganiserPage: NextPageAuth = observer(() => {

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
                    <OrganiserForm/>
                }
            </Container>

        </>
    )
})

CreateOrganiserPage.requireAuth = true;

export default CreateOrganiserPage;