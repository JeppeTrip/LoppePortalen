import { Button, CircularProgress, Container, Stack, Typography } from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import OrganiserForm from "../../../components/OrganiserForm";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import { useContext, useEffect, useState } from "react";
import { useRouter } from "next/router";
import { LoadingButton } from "@mui/lab";
import SaveIcon from '@mui/icons-material/Save';
import { StoreContext } from "../../../NewStores/StoreContext";

type Props = {
    id: string
}

const EditOrganiserPage: NextPageAuth<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const [organiserId, setOrganiserId] = useState<string>(undefined);
    const router = useRouter();

    //mount
    useEffect(() => {

    }, [])

    //Unmount
    useEffect(() => {
        return () => {
            setOrganiserId(undefined);
        }
    }, [])

    useEffect(() => {
        if (!router.isReady) {
            return
        };
        var { id } = router.query
        setOrganiserId(id + "")
    }, [router.isReady]);

    /**
     * If you come here directly and edit organiser in the organiser store hasn't been set to anything
     * check if there is something in the current user. 
     * else go and check the backend
     * if nothing in the backend go to error page.
     */
    useEffect(() => {
        if (organiserId) {
            if (!stores.organiserStore.selectedOrganiser || stores.organiserStore.selectedOrganiser == null) {
                
            }
        }
    }, [organiserId])

    const handleSubmit = () => {
        
    }

    const handleReset = () => {

    }

    return (
        <Container
            style={{ paddingTop: "25px" }}
            maxWidth="sm">
            <Stack spacing={1}>
                {
                    (stores.organiserStore.editedOrganiser) && <OrganiserForm organiser={stores.organiserStore.editedOrganiser} />
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
                <Button
                    onClick={() => handleReset()}
                >
                    Reset
                </Button>
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

EditOrganiserPage.requireAuth = true;

export default EditOrganiserPage;