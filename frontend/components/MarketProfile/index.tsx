import { Container, Divider, Grid, Paper, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Market } from "../../NewStores/@DomainObjects/Market";
import StallDisplay from "./StallDisplay";
import styles from './styles.module.css';

type Props = {
    market: Market
}

const MarketProfile: FC<Props> = observer((props: Props) => {
    return (
        <Grid id={`market_${props.market.id}_grid`} container columns={1} spacing={1}>
            <Grid item xs={1}>
                <Paper square={true} elevation={1}>
                    <Container maxWidth={"xl"}>
                        <Grid container columns={12}>
                            <Grid item xs={12} container justifyContent={"center"}>
                                {
                                    props.market.imageData ? <img className={styles.banner} src={`data:image/jpeg;base64,${props.market.imageData}`} /> : <div className={styles.bannerPlaceholder} />
                                }
                            </Grid>
                            <Grid item xs={12}>
                                <Grid container columns={12}>
                                    <Grid item xs={8}>
                                        <Grid>
                                            <Grid item xs={12}>
                                                <Typography variant="h5">
                                                    {props.market.name}
                                                </Typography>
                                            </Grid>
                                            <Grid item xs={12}>
                                                <Typography>
                                                    {
                                                        props.market.startDate.toLocaleDateString() + " - " + props.market.endDate.toLocaleDateString()
                                                    }
                                                </Typography>
                                            </Grid>
                                            {
                                                (props.market.location) && <Grid item xs={12}>
                                                    <Typography>
                                                        {
                                                            `${props.market.location.address}, ${props.market.location.postalCode} ${props.market.location.city}`
                                                        }
                                                    </Typography>
                                                </Grid>
                                            }
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
                                <div className={styles.AboutInfo}>
                                    <Typography variant="h6">
                                        About
                                    </Typography>
                                    <Divider />
                                    <Typography variant="body1">
                                        {props.market.description}
                                    </Typography>
                                </div>
                            </Paper>
                        </Grid>
                        <Grid item xs={5}>
                            <Paper elevation={1}>
                                <StallDisplay market={props.market} />
                            </Paper>
                        </Grid>
                    </Grid>
                </Container>
            </Grid>
        </Grid>
    )
})

export default MarketProfile