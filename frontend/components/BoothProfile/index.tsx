import { Container, Divider, Grid, Paper, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Booth } from "../../NewStores/@DomainObjects/Booth";
import styles from './styles.module.css';


type Props = {
    booth: Booth
}

const BoothProfile: FC<Props> = observer((props: Props) => {
    return (
        <Grid id={`booth_${props.booth.id}_grid`} container columns={1} spacing={1}>
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
                                                    {props.booth.name}
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
                                <div className={styles.AboutInfo}>
                                    <Typography variant="h6">
                                        About
                                    </Typography>
                                    <Divider />
                                    <Typography variant="body1">
                                        {props.booth.description}
                                    </Typography>
                                </div>
                            </Paper>
                        </Grid>
                    </Grid>
                </Container>
            </Grid>
        </Grid>
    )
})

export default BoothProfile