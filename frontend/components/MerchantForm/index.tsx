import SaveIcon from "@mui/icons-material/Save";
import { LoadingButton } from "@mui/lab";
import { Grid, TextField } from '@mui/material';
import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { useRouter } from 'next/router';
import { FC, useCallback, useEffect, useState } from 'react';
import { ModelState } from "../../@types/ModelState";
import { Merchant } from "../../NewStores/@DomainObjects/Merchant";


type Props = {
    merchant : Merchant
    title: string
}

const theme = createTheme();

const MerchantForm: FC<Props> = (props: Props) => {
    const [activeStep, setActiveStep] = useState(0);
    const router = useRouter()

    const redirect = useCallback(() => {
        if(router.isReady)
        {
            router.push("/profile", undefined, {shallow: true})
        }
    }, [router.isReady])

    useEffect(() => {
        if(props.merchant.state === ModelState.IDLE)
        {
            redirect()
        }
    }, [props.merchant.state])

    return (
        <ThemeProvider theme={theme}>
            <Container component="main" maxWidth="md">
                <Paper variant="outlined" sx={{ p: { xs: 2, md: 3 } }}>

                    <Grid container spacing={2}>
                        <Grid item>
                            <Typography component="h1" variant="h4" align="center">
                                {props.title}
                            </Typography>
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                fullWidth={true}
                                id="merchantName"
                                label="Name"
                                variant="outlined"
                                onChange={(event) => props.merchant.name = event.target.value}
                                value={props.merchant.name}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField fullWidth={true}
                                id="merchant Description"
                                label="Description"
                                variant="outlined"
                                onChange={(event) => props.merchant.description = event.target.value}
                                value={props.merchant.description}
                                multiline
                                rows={10}
                            />
                        </Grid>
                    </Grid>
                    <Grid>
                        <Grid item>
                            <LoadingButton
                                onClick={() => props.merchant.save()}
                                loading={props.merchant.state === ModelState.SAVING}
                                loadingPosition="start"
                                startIcon={<SaveIcon />}
                                variant="contained"
                                sx={{ mt: 3, ml: 1 }}
                            >
                                Submit
                            </LoadingButton>
                        </Grid>
                        {
                            props.merchant.state === ModelState.ERROR &&
                            (
                                <Grid item>
                                    <Typography variant="caption" color="red">
                                        Something went wrong.
                                        Unable to submit new market.
                                    </Typography>
                                </Grid>
                            )
                        }
                    </Grid>
                </Paper>
            </Container>
        </ThemeProvider>
    );
}


export default observer(MerchantForm);