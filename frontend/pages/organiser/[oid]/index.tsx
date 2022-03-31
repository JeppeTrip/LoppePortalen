import { CircularProgress, Container, Divider, Grid, List, Paper, Typography } from "@mui/material";
import { styled } from "@mui/system";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useContext, useEffect, useState } from "react";
import TopBar from "../../../components/TopBar";
import { StoreContext } from "../../../stores/StoreContext";
import styles from './styles.module.css'
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import { useRouter } from "next/router";
import { LoadingButton } from "@mui/lab";
import CancelIcon from '@mui/icons-material/Cancel';
import MarketListItem from "../../../components/MarketListItem";

type Props = {
    oid: string
}

const OrganiserProfilePage: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const [selectedOrganiser, setSelectedOrganiser] = useState(null)
    const [organiserId, setOrganiserId] = useState<string>("");
    const router = useRouter();

    useEffect(() => {
        if (!router.isReady) { return };
        var { oid } = router.query
        setOrganiserId(oid + "")

    }, [router.isReady]);

    useEffect(() => {
        if (stores.organiserStore.selectedOrganiser == null) {
            if (!(organiserId == "")) {
                stores.organiserStore.loadOrganiser(parseInt(organiserId));
            }
        }
    }, [organiserId])

    useEffect(() => {
        if (stores.organiserStore.selectedOrganiser != null) {
            setSelectedOrganiser(stores.organiserStore.selectedOrganiser)
        }
    }, [stores.organiserStore.selectedOrganiser])

    //Adding a return statement is the same as componentWillUnMount - that is pretty damn obscure.
    useEffect(() => {
        return () => {
            stores.organiserStore.selectedOrganiser = null;
        }
    }, [])

    //Load markets when component mounts
    useEffect(() => {
        //Todo: error handling with shitty organiser id.
        const id = parseInt(organiserId)
        if (organiserId != "" && !isNaN(id))
            stores.marketStore.getFilteredMarkets(id, true, new Date(), new Date("2200-12-31T23:59:00"),);
    }, [organiserId])

    const markets = () => {
        return (
            <Paper elevation={1}>
                <Container>
                    <Typography variant="h6">
                        Upcomming Markets
                    </Typography>
                    <Divider />
                    <List>
                        {
                            stores.marketStore.markets.map(
                                market => <> <MarketListItem Market={market} /> <Divider /> </>)
                        }
                    </List>
                </Container>
            </Paper>
        );
    }

    return (
        <>
            {
                selectedOrganiser == null ? <CircularProgress /> :
                    <Grid container columns={1} spacing={1}>
                        <Grid item xs={1}>
                            <Paper square={true} elevation={1}>
                                <Container maxWidth={"xl"}>
                                    <Grid container columns={12}>
                                        <Grid item xs={12}>
                                            <div className={styles.bannerPlaceholder} />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Grid container columns={12}>
                                                <Grid item xs={8}>
                                                    <Grid>
                                                        <Grid item xs={12}>
                                                            <Typography variant="h5">
                                                                {selectedOrganiser.name}
                                                            </Typography>
                                                        </Grid>
                                                    </Grid>
                                                </Grid>
                                            </Grid>
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
                                            <Container>
                                                <Typography variant="h6">
                                                    About
                                                </Typography>
                                                <Divider />
                                                <Typography variant="body1">
                                                    {selectedOrganiser.description}
                                                </Typography>
                                            </Container>
                                        </Paper>
                                    </Grid>
                                    <Grid item xs={5}>
                                        {
                                            markets()
                                        }
                                    </Grid>
                                </Grid>
                            </Container>
                        </Grid>
                    </Grid >
            }


        </>
    )
})

export default OrganiserProfilePage;