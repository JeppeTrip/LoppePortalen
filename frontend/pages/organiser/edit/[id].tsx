import SaveIcon from '@mui/icons-material/Save';
import { LoadingButton } from "@mui/lab";
import { Button, Container, Stack, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import OrganiserForm from "../../../components/OrganiserForm";
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
                    (stores.organiserStore.selectedOrganiser) && <OrganiserForm organiser={stores.organiserStore.selectedOrganiser} />
                }
                <LoadingButton
                    onClick={handleSubmit}
                    loading={false}
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
                    false &&
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