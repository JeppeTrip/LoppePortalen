import { CircularProgress, Container } from "@mui/material";
import { observer } from "mobx-react-lite";
import { redirect } from "next/dist/server/api-utils";
import { useRouter } from "next/router";
import { useCallback, useContext, useEffect, useState } from "react";
import { ModelState } from "../../../@types/ModelState";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import MerchantForm from "../../../components/MerchantForm";
import { Merchant } from "../../../NewStores/@DomainObjects/Merchant";
import { StoreContext } from "../../../NewStores/StoreContext";

const CreateMerchantPage: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext);
    const [newMerchant, setNewMerchant] = useState<Merchant>()
    const router = useRouter();

    //Component mounts
    useEffect(() => {
        setNewMerchant(stores.merchantStore.createMerchant())
    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {

        }
    }, [])

    return (
        <>
            <Container
                style={{ paddingTop: "25px" }}
                maxWidth="sm">
                {
                    newMerchant != null && <MerchantForm merchant={newMerchant} title={"Create New Salesprofile"}/>
                }
            </Container>
        </>
    )
})

CreateMerchantPage.requireAuth = true;

export default CreateMerchantPage;