import { Container, Divider, Grid, Paper, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Merchant } from "../../NewStores/@DomainObjects/Merchant";
import BoothListItem from "../BoothListItem";
import styles from './styles.module.css';


type Props = {
    merchant : Merchant
}


const MerchantProfile: FC<Props> = observer((props: Props) => {
    return (
        <Grid id={`merchant_${props.merchant.id}_profile_grid`} container columns={1} spacing={1}>
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
                                <Typography variant="h6">
                                    About
                                </Typography>
                                <Divider />
                                <Typography variant="body1">
                                    {props.merchant.description}
                                </Typography>
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