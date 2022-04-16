import { Container, Stack } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect } from "react";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import OrganiserForm from "../../../components/OrganiserForm";
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