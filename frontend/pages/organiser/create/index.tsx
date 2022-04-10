import { CircularProgress, Container, Stack, Typography } from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import OrganiserForm from "../../../components/OrganiserForm";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import { useContext, useEffect } from "react";
import { StoreContext } from "../../../stores/StoreContext";
import { Organiser } from "../../../@types/Organiser";
import { LoadingButton } from "@mui/lab";
import SaveIcon from '@mui/icons-material/Save';
import { useRouter } from "next/router";

const CreateOrganiserPage: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    //mount
    useEffect(() => {
        stores.organiserStore.resetSubmitState()
        stores.organiserStore.setNewOrganiser(new Organiser(
            undefined,
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        ))
    }, [])

    //Unmount
    useEffect(() => {
        return () => {
            stores.organiserStore.setNewOrganiser(new Organiser(
                undefined,
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            ))
        }
    }, [])

    //go back when submission is done.
    useEffect(() => {
        if (stores.organiserStore.submitSuccess) {
            router.back()
        }
    }, [stores.organiserStore.submitSuccess])

    const handleSubmit = () => {
        stores.organiserStore.addOrganiser(stores.organiserStore.newOrganiser);
    }

    return (
        <Container
            style={{ paddingTop: "25px" }}
            maxWidth="sm">
            <Stack spacing={1}>
                {
                    <OrganiserForm organiser={stores.organiserStore.newOrganiser} />
                }
                <LoadingButton
                    onClick={handleSubmit}
                    loading={stores.organiserStore.isSubmitting}
                    loadingPosition="start"
                    startIcon={<SaveIcon />}
                    variant="contained"

                >
                    Submit
                </LoadingButton>
                {
                    //TODO: Make error handling waaay the fuck better.
                    stores.organiserStore.hadSubmissionError &&
                    <Typography variant="caption" color={"red"}>
                        Could not submit.
                    </Typography>
                }
            </Stack>
        </Container>
    )
})

CreateOrganiserPage.requireAuth = true;

export default CreateOrganiserPage;