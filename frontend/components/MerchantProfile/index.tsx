import { TabContext, TabList, TabPanel } from "@mui/lab";
import { Box, Container, Divider, Grid, Paper, Tab, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useState } from "react";
import { Merchant } from "../../NewStores/@DomainObjects/Merchant";
import BoothListItem from "../BoothListItem";
import ContactInfoListItem from "../ContactInfoListItem";
import styles from './styles.module.css';


type Props = {
    merchant : Merchant
}


const MerchantProfile: FC<Props> = observer((props: Props) => {
    const [tabValue, setTabValue] = useState('1')

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };


    return (
        <Grid id={`merchant_${props.merchant.id}_profile_grid`} container columns={1} spacing={1}>
            <Grid item xs={1}>
                <Paper square={true} elevation={1}>
                    <Container maxWidth={"xl"}>
                        <Grid container columns={12}>
                            <Grid item xs={12} container justifyContent={"center"}>
                                {
                                    props.merchant.imageData ? <img className={styles.banner} src={`data:image/jpeg;base64,${props.merchant.imageData}`} /> : <div className={styles.bannerPlaceholder} />
                                }
                            </Grid>
                            <Grid item xs={12}>
                                <Grid container columns={12}>
                                    <Grid item xs={8}>
                                        <Grid>
                                            <Grid item xs={12}>
                                                <Typography variant="h5">
                                                    {props.merchant.name}
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
                                        <TabList onChange={handleTabChange}>
                                            <Tab label="Merchant Info" value="1" />
                                            <Tab label="Contact Info" value="2" />
                                        </TabList>
                                    </Box>
                                    <TabPanel value="1">
                                        {
                                            <Container>
                                                <Typography variant="body1">
                                                    {props.merchant.description}
                                                </Typography>
                                            </Container>
                                        }
                                    </TabPanel>
                                    <TabPanel value="2">
                                        {
                                            <Container>
                                                {
                                                    props.merchant.contactInfo.length === 0 ?
                                                        (
                                                            "no contact info found"
                                                        )
                                                        :
                                                        (
                                                            props.merchant.contactInfo.map(x => <ContactInfoListItem key={`${props.merchant.id}_${x.value}`} contactInfo={x} />)
                                                        )
                                                }
                                            </Container>
                                        }
                                    </TabPanel>
                                </TabContext>
                            </Paper>
                        </Grid>

                        <Grid item xs={5}>
                            <Paper elevation={1}>
                                <Typography variant="h6">
                                    Merchant Booths
                                </Typography>
                                <Divider />
                                {
                                    props.merchant.booths.map(x => <BoothListItem booth={x} />)
                                }
                            </Paper>
                        </Grid>
                    </Grid>
                </Container>
            </Grid>
        </Grid>
    )
})

export default MerchantProfile