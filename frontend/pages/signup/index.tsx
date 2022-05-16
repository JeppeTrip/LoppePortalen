import { Container } from "@mui/material";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import UserForm from "../../components/UserForm";
import { Auth } from "../../NewStores/@DomainObjects/Auth";
import { StoreContext } from "../../NewStores/StoreContext";


const SingupPage: NextPage = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter()
    const [newAuth, setNewAuth] = useState<Auth>()

    useEffect(() => {
        setNewAuth(stores.authStore.createAuth())
    }, [])

    useEffect(() => {
        if(stores.authStore.auth.signedIn)
        {
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
                    (!stores.authStore.auth.signedIn && newAuth != null) && <UserForm auth={newAuth}/>
                }
            </Container>
        </>
    )
})

export default SingupPage;