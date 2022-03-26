import { CircularProgress, Container, Grid, Paper } from "@mui/material";
import { styled } from "@mui/system";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useContext, useEffect, useState } from "react";
import TopBar from "../../../components/TopBar";
import { StoreContext } from "../../../stores/StoreContext";
import styles from './styles.module.css'

type Props = {
    mid: string
}

const MarketProfilePageID: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const [selectedMarket, setSelectedMarket] = useState(null)

    useEffect(() => {
        setSelectedMarket(stores.marketStore.selectedMarket)
    }, [stores.marketStore.selectedMarket])

    return (
        <>
            {
                selectedMarket == null ? <CircularProgress /> : <Container>
                <Grid container columns={12} spacing={1}>
                    <Grid item xs={7}>
                        <Paper elevation={1}>
                            {selectedMarket.description}
                        </Paper>
                    </Grid>
                    <Grid item xs={5}>
                        <Paper elevation={1}>
                            "SOMETHING ELOSE HERHE"
                        </Paper>
                    </Grid>
                </Grid>
            </Container>
            }
            

        </>
    )
})

export default MarketProfilePageID;