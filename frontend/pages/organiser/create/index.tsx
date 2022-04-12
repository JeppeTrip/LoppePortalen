import { CircularProgress, Container, Stack, Typography } from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import OrganiserForm from "../../../components/OrganiserForm";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import { useContext, useEffect } from "react";
import { Organiser } from "../../../@types/Organiser";
import { LoadingButton } from "@mui/lab";
import SaveIcon from '@mui/icons-material/Save';
import { useRouter } from "next/router";
import { StoreContext } from "../../../NewStores/StoreContext";

const CreateOrganiserPage: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    //mount
    useEffect(() => {

    }, [])

    //Unmount
    useEffect(() => {
        return () => {

        }
    }, [])

    return (
        <Container
            style={{ paddingTop: "25px" }}
            maxWidth="sm">
            <Stack spacing={1}>
                {
                    <OrganiserForm organiser={stores.organiserStore.createOrganiser()} />
                }
            </Stack>
        </Container>
    )
})

CreateOrganiserPage.requireAuth = true;

export default CreateOrganiserPage;