import { Container, Stack } from "@mui/material";
import { reaction } from "mobx";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { ModelState } from "../../../@types/ModelState";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import OrganiserForm from "../../../components/OrganiserForm";
import { Organiser } from "../../../NewStores/@DomainObjects/Organiser";
import { StoreContext } from "../../../NewStores/StoreContext";

const CreateOrganiserPage: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext);
    const [newOrganiser, setNewOrganiser] = useState<Organiser>(stores.organiserStore.createOrganiser())
    const router = useRouter();

    //mount
    useEffect(() => {
        console.log("component mounts")
    }, [])

    //Unmount
    useEffect(() => {
        return () => {
            //if new organiser state is still new remove from the organiser array.
        }
    }, [])

    /**When the organiser has been succesfully created. */
    reaction(
        () => newOrganiser.state,
        state => {
            if(state === ModelState.IDLE)
            {
                router.push(`show/${newOrganiser.id}`, undefined,  { shallow: true })
            }
        }
    )

    return (
        <Container
            style={{ paddingTop: "25px" }}
            maxWidth="sm">
            <Stack spacing={1}>
                {
                    <OrganiserForm organiser={newOrganiser} />
                }
            </Stack>
        </Container>
    )
})

CreateOrganiserPage.requireAuth = true;

export default CreateOrganiserPage;