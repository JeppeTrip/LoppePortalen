import { CircularProgress } from "@mui/material";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { useErrorHandler } from 'react-error-boundary';
import OrganiserProfile from "../../../components/OrganiserProfile";
import { Organiser } from "../../../NewStores/@DomainObjects/Organiser";
import { StoreContext } from "../../../NewStores/StoreContext";



type Props = {
    oid: string
}

const OrganiserProfilePage: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler();
    const [organiserId, setOrganiserId] = useState<string>("");
    const [selectedOrganiser, setSelectedOrganiser] = useState<Organiser>(null)
    const router = useRouter();


    useEffect(() => {
        if (!router.isReady) { return };
        var { oid } = router.query
        setOrganiserId(oid + "")
    }, [router.isReady]);

    useEffect(() => {
        console.log("ever called ?")
        if (selectedOrganiser == null) {
            if (!(organiserId == "")) {
                flowResult(stores.organiserStore.fetchOrganiser(parseInt(organiserId)))
                    .then(res => {
                        setSelectedOrganiser(res)
                    }).catch(error => {
                        console.log("error ?")
                        handleError(error)
                    });
            }
        }
    }, [organiserId, selectedOrganiser])

    return (
        <>
            {
                selectedOrganiser == null ? <CircularProgress /> : <OrganiserProfile organiser={selectedOrganiser} />
            }
        </>
    )
})

export default OrganiserProfilePage;