import { Box, CircularProgress, Container, Divider, Grid, List, Paper, Tab, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { flowResult } from "mobx";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import MarketListItem from "../../../components/MarketListItem";
import { StoreContext } from "../../../NewStores/StoreContext";
import styles from './styles.module.css';
import { useErrorHandler } from 'react-error-boundary';
import { TabContext, TabList, TabPanel } from "@mui/lab";
import ContactInfoListItem from "../../../components/ContactInfoListItem";


type Props = {
    oid: string
}

const OrganiserProfilePage: NextPage<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler();
    const [organiserId, setOrganiserId] = useState<string>("");
    const router = useRouter();
    const [tabValue, setTabValue] = useState('1')

    useEffect(() => {
        if (!router.isReady) { return };
        var { oid } = router.query
        setOrganiserId(oid + "")
    }, [router.isReady]);

    useEffect(() => {
        if (stores.organiserStore.selectedOrganiser == null) {
            if (!(organiserId == "")) {
                flowResult(stores.organiserStore.fetchOrganiser(parseInt(organiserId)))
                    .then(res => {
                        res.select()
                    }).catch(error => {
                        handleError(error)
                    });
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

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

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
                                            <TabContext value={tabValue}>
                                                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                                                    <TabList onChange={handleTabChange} aria-label="lab API tabs example">
                                                        <Tab label="Organiser Info" value="1" />
                                                        <Tab label="Contact Info" value="2" />
                                                    </TabList>
                                                </Box>
                                                <TabPanel value="1">
                                                    {
                                                        <Container>
                                                            <Typography variant="body1">
                                                                {stores.organiserStore.selectedOrganiser.description}
                                                            </Typography>
                                                        </Container>
                                                    }
                                                </TabPanel>
                                                <TabPanel value="2">
                                                    {
                                                        <Container>
                                                            {
                                                                stores.organiserStore.selectedOrganiser.contactInfo.length === 0 ?
                                                                    (
                                                                        "no contact info found"
                                                                    )
                                                                    :
                                                                    (
                                                                        stores.organiserStore.selectedOrganiser.contactInfo.map(x => <ContactInfoListItem key={`${stores.organiserStore.selectedOrganiser.id}_${x.value}`} contactInfo={x} />)
                                                                    )
                                                            }
                                                        </Container>
                                                    }
                                                </TabPanel>
                                            </TabContext>
                                            <Container>

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