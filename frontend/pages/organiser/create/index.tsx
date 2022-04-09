import {CircularProgress, Container} from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import OrganiserForm from "../../../components/OrganiserForm";

const CreateOrganiserPage: NextPage = observer(() => {

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

export default CreateOrganiserPage;