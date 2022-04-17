import { CircularProgress, Container } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect } from "react";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import MerchantForm from "../../../components/MerchantForm";
import { StoreContext } from "../../../NewStores/StoreContext";

const CreateMerchantPage: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    //Component mounts
    useEffect(() => {

    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {

        }
    }, [])

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
                    <MerchantForm title={"Create New Salesprofile"}/>
                }
            </Container>
        </>
    )
})

CreateMerchantPage.requireAuth = false;

export default CreateMerchantPage;