import { CircularProgress, Container, Divider, Grid, Paper, Typography } from "@mui/material";
import { styled } from "@mui/system";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useContext, useEffect, useState } from "react";
import TopBar from "../../../components/TopBar";
import { StoreContext } from "../../../stores/StoreContext";
import styles from './styles.module.css'
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';

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
                selectedMarket == null ? <CircularProgress /> :
                    <Grid container columns={1} spacing={1}>
                        <Grid item xs={1}>
                            <Paper square={true} elevation={1}>
                                <Container maxWidth={"xl"}>
                                    <Grid container columns={12}>
                                        <Grid item xs={12}>
                                            <div className={styles.bannerPlaceholder} />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Typography variant="h5">
                                                {selectedMarket.name}
                                            </Typography>
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Typography>
                                                {
                                                    selectedMarket.startDate.toLocaleDateString() + " - " + selectedMarket.endDate.toLocaleDateString()
                                                }
                                            </Typography>
                                        </Grid>
                                    </Grid>
                                </Container>
                            </Paper>
                        </Grid>

                        <Grid item xs={1}>
                            <Container maxWidth={"lg"}>
                                <Grid container columns={12} spacing={1}>
                                    <Grid item xs={7}>
                                        <Paper elevation={1}>
                                            <div className={styles.AboutInfo}>
                                                <Typography variant="h6">
                                                    About
                                                </Typography>
                                                <Divider />
                                                <Typography variant="body1">
                                                    {selectedMarket.description}
                                                </Typography>
                                            </div>
                                        </Paper>
                                    </Grid>
                                    <Grid item xs={5}>
                                        <Paper elevation={1}>
                                            <div className={styles.mapPlaceholder} />
                                        </Paper>
                                    </Grid>
                                </Grid>
                            </Container>
                        </Grid>
                    </Grid>
            }


        </>
    )
})

export default MarketProfilePageID;