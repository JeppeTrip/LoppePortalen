import { Container, Paper, Typography } from "@mui/material";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { useErrorHandler } from "react-error-boundary";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import BoothForm from "../../../components/BoothForm";
import { Booth } from "../../../NewStores/@DomainObjects/Booth";
import { StoreContext } from '../../../NewStores/StoreContext';


type Props = {
    id: string
}

const EditBoothPage: NextPageAuth<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler()
    const router = useRouter();
    const [selectedBooth, setSelectedBooth] = useState<Booth>(null)
    const [boothId, setBoothId] = useState<string>("");

    //mount
    useEffect(() => {
        stores.itemCategoryStore.fetchCategories()
    }, [])

    useEffect(() => {
        if (!router.isReady) {
            return
        };
        var { id } = router.query
        setBoothId(id + "")
    }, [router.isReady]);

    useEffect(() => {
        if (selectedBooth == null) {
            if (!(boothId === "")) {
                flowResult(stores.boothStore.fetchBooth(boothId))
                    .then(res => {
                        setSelectedBooth(res)
                    })
                    .catch(error => {
                        handleError(error)
                    })
            }
        }
    }, [boothId, selectedBooth])

    return (
        <Container>
            <Paper elevation={1} sx={{ p: 2 }}>
                <Typography variant="h2">
                    Edit Boothpage
                </Typography>
                {
                    selectedBooth != null && <BoothForm booth={selectedBooth} />
                }
            </Paper>
        </Container>

    )
})

EditBoothPage.requireAuth = true;

export default EditBoothPage;