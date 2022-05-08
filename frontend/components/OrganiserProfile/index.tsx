import { Box, CircularProgress, Container, Divider, Grid, List, Paper, Tab, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useState } from "react";
import { Organiser } from "../../NewStores/@DomainObjects/Organiser";
import styles from './styles.module.css';
import { TabContext, TabList, TabPanel } from "@mui/lab";
import ContactInfoListItem from "../ContactInfoListItem";
import MarketListItem from "../MarketListItem";
type Props = {
    organiser: Organiser
}

const OrganiserProfile: FC<Props> = observer((props: Props) => {
    const [tabValue, setTabValue] = useState('1')

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    return (
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
                                                    {props.organiser.name}
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
                                                    {props.organiser.description}
                                                </Typography>
                                            </Container>
                                        }
                                    </TabPanel>
                                    <TabPanel value="2">
                                        {
                                            <Container>
                                                {
                                                    props.organiser.contactInfo.length === 0 ?
                                                        (
                                                            "no contact info found"
                                                        )
                                                        :
                                                        (
                                                            props.organiser.contactInfo.map(x => <ContactInfoListItem key={`${props.organiser.id}_${x.value}`} contactInfo={x} />)
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
                                <Paper elevation={1}>
                                    <Container>
                                        <Typography variant="h6">
                                            Upcomming Markets
                                        </Typography>
                                        <Divider />
                                        <List>
                                            {
                                                props.organiser.markets.map(x =>
                                                    <>
                                                        <MarketListItem Market={x} /> <Divider />
                                                    </>
                                                )
                                            }
                                        </List>
                                    </Container>
                                </Paper >
                            }
                        </Grid>
                    </Grid>
                </Container>
            </Grid>
        </Grid >
    )
})

export default OrganiserProfile