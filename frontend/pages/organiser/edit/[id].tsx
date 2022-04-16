import SaveIcon from '@mui/icons-material/Save';
import { LoadingButton } from "@mui/lab";
import { Button, Container, Stack, Typography } from "@mui/material";
import { flowResult, reaction } from 'mobx';
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useCallback, useContext, useEffect, useMemo, useState } from "react";
import { ModelState } from '../../../@types/ModelState';
import { NextPageAuth } from "../../../@types/NextAuthPage";
import OrganiserForm from "../../../components/OrganiserForm";
import { Organiser } from '../../../NewStores/@DomainObjects/Organiser';
import { StoreContext } from "../../../NewStores/StoreContext";

type Props = {
    id: string
}

const EditOrganiserPage: NextPageAuth<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const [organiserId, setOrganiserId] = useState<string>(undefined);
    const [selectedOrganiser, setSelectedOrganiser] = useState<Organiser>(new Organiser(null))
    const router = useRouter();

    const resetOrganiserState = useCallback(() => {
        if(selectedOrganiser != null) 
            selectedOrganiser.state = ModelState.IDLE
    }, [selectedOrganiser]);

    //mount
    useEffect(() => {

    }, [])

    //Unmount
    useEffect(() => {
        return () => {
            resetOrganiserState()
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
            flowResult(stores.organiserStore.fetchOrganiser(parseInt(organiserId)))
            .then(organiser => {
                organiser.state = ModelState.EDITING
                setSelectedOrganiser(organiser)
                
            });
        }
    }, [organiserId])

    useEffect(() => {
        if(selectedOrganiser.state === ModelState.IDLE)
            router.push(`/profile`, undefined, {shallow: true})
    }, [selectedOrganiser.state])

    return (
        <Container
            style={{ paddingTop: "25px" }}
            maxWidth="sm">
            <Stack spacing={1}>
                {
                    (selectedOrganiser != null) && <OrganiserForm organiser={selectedOrganiser} />
                }
            </Stack>
        </Container>
    )
})

EditOrganiserPage.requireAuth = true;

export default EditOrganiserPage;