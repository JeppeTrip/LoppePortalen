import { CircularProgress, Container, Divider, Grid, List, Paper, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import MarketListItem from "../../../components/MarketListItem";
import { StoreContext } from "../../../NewStores/StoreContext";
import styles from './styles.module.css';


type Props = {
    oid: string
}

const OrganiserProfilePage: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
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
                stores.organiserStore.resolveSelectedOrganiser(parseInt(organiserId))
            }
        }
    }, [organiserId, stores.organiserStore.selectedOrganiser])

    //Adding a return statement is the same as componentWillUnMount - that is pretty damn obscure.
    useEffect(() => {
        return () => {

        }
    }, [])

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
                            stores.organiserStore.selectedOrganiser.markets.map(x =>
                                <>
                                    <MarketListItem Market={x} /> <Divider />
                                </>
                            )

                        }
                    </List>
                </Container>
            </Paper >
        );
    }

    return (
        <>
            {
                stores.organiserStore.selectedOrganiser == null ? <CircularProgress /> :
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
                                                                {stores.organiserStore.selectedOrganiser.name}
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
                                                    {stores.organiserStore.selectedOrganiser.description}
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