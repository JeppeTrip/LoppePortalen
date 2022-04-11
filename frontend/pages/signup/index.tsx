import {CircularProgress, Container } from "@mui/material";

import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import UserForm from "../../components/UserForm";
import { StoreContext } from "../../NewStores/StoreContext";
import { useContext, useEffect } from "react";
import { useRouter } from "next/router";

const SingupPage: NextPage = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter()

    useEffect(() => {
        
    }, [])

    useEffect(() => {
        console.log("should redirect")
        console.log(stores.authStore.auth.signedIn)
        if(stores.authStore.auth.signedIn)
        {
            console.log("auth is signed in.")
            if(router.isReady){
                router.push("/profile", undefined, { shallow: true })
            }
        }
    }, [stores.authStore.auth.signedIn, router.isReady])

    return (
        <>
            <Container
                style={{ paddingTop: "25px" }}
                maxWidth="sm">
                {
                    !stores.authStore.auth.signedIn && <UserForm auth={stores.authStore.createAuth()}/>
                }
            </Container>
        </>
    )
})

export default SingupPage;