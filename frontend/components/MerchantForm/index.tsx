import SaveIcon from "@mui/icons-material/Save";
import { LoadingButton } from "@mui/lab";
import { Grid, TextField } from '@mui/material';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Stepper from '@mui/material/Stepper';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { useRouter } from 'next/router';
import { FC, Fragment, useEffect, useState } from 'react';
import { ModelState } from '../../@types/ModelState';
import { Market } from '../../NewStores/@DomainObjects/Market';
import MarketDetailsForm from '../MarketDetailsForm';
import StallForm from '../StallForm';


type Props = {
    title: string
}

const steps = ['Market Information'];

const theme = createTheme();

const MerchantForm: FC<Props> = (props: Props) => {
    const [activeStep, setActiveStep] = useState(0);
    const router = useRouter()

    //on mount
    useEffect(() => {

    }, [])

    //unmount
    useEffect(() => {
        return () => {

        }
    }, [])

    useEffect(() => {

    }, [])

    const handleNext = () => {
        setActiveStep(activeStep + 1);
    };

    const handleBack = () => {
        setActiveStep(activeStep - 1);
    };

    const getButton = () => {
        return (
            activeStep === steps.length - 1 ? loadingButton() : nextButton()
        )
    }

    const handleSubmit = (event) => {

    }

    const getStepContent = (step: number) => {

    }

    const loadingButton = () => <LoadingButton
        onClick={handleSubmit}
        loading={false}
        loadingPosition="start"
        startIcon={<SaveIcon />}
        variant="contained"
        sx={{ mt: 3, ml: 1 }}
    >
        Submit
    </LoadingButton>

    const nextButton = () => <Button
        variant="contained"
        onClick={handleNext}
        sx={{ mt: 3, ml: 1 }}
    >
        Next
    </Button>

    const handleReset = () => {

    }

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
                                onChange={(event) => console.log(event.target.value)}
                                value={""}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField fullWidth={true}
                                id="merchant Description"
                                label="Description"
                                variant="outlined"
                                onChange={(event) => console.log(event.target.value)}
                                value={""}
                                multiline
                                rows={10}
                            />
                        </Grid>
                    </Grid>
                    <Grid>
                        <Grid item>
                            <LoadingButton
                                onClick={() => console.log("handle submit")}
                                loading={false}
                                loadingPosition="start"
                                startIcon={<SaveIcon />}
                                variant="contained"
                                sx={{ mt: 3, ml: 1 }}
                            >
                                Submit
                            </LoadingButton>
                        </Grid>
                        {
                            false &&
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