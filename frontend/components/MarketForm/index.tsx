import SaveIcon from "@mui/icons-material/Save";
import { LoadingButton } from "@mui/lab";
import { Grid } from '@mui/material';
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


type Props = {
    market: Market
    title?: string
    editing?: boolean
}

const steps = ['Market Information'];

const theme = createTheme();

const MarketForm: FC<Props> = (props: Props) => {
    const [activeStep, setActiveStep] = useState(0);
    const router = useRouter()

    //on mount
    useEffect(() => {
        if (props.editing) {
            props.market.state = ModelState.EDITING
        }
        else {
            props.market.state = ModelState.NEW
        }
    }, [])

    //unmount
    useEffect(() => {
        return () => {

        }
    }, [])

    useEffect(() => {
        if (props.market.state === ModelState.IDLE) {
            if (router.isReady) {
                router.push('/market/' + props.market.id, undefined, { shallow: true });
            }
        }
    }, [props.market.state])

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
        props.market.save();
    }

    const getStepContent = (step: number) => {
        switch (step) {
            case 0:
                return <MarketDetailsForm market={props.market} />;
            //case 1: return <StallForm market={props.market} />;
            default:
                throw new Error('Unknown step');
        }
    }

    const loadingButton = () => <LoadingButton
        onClick={handleSubmit}
        loading={props.market.state === ModelState.SAVING}
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
        props.market.resetState()
    }

    return (
        <ThemeProvider theme={theme}>
            <Container component="main" maxWidth="md">
                <Paper variant="outlined" sx={{ p: { xs: 2, md: 3 } }}>
                    <Typography component="h1" variant="h4" align="center">
                        {props.title}
                    </Typography>
                    <Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}>
                        {steps.map((label) => (
                            <Step key={label}>
                                <StepLabel>{label}</StepLabel>
                            </Step>
                        ))}
                    </Stepper>
                    <Fragment>
                        <Fragment>
                            {getStepContent(activeStep)}
                            <Grid container>
                                {
                                    props.editing && <Grid item container xs>
                                        <Button onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
                                            Reset
                                        </Button>
                                    </Grid>
                                }
                                <Grid item>
                                    {activeStep !== 0 && (
                                        <Button disabled={false} onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
                                            Back
                                        </Button>
                                    )}
                                    {getButton()}
                                </Grid>
                            </Grid>
                            {
                                props.market.state === ModelState.ERROR &&
                                (
                                    <Grid>
                                        <Grid item>
                                            <Typography variant="caption" color="red">
                                                Something went wrong.
                                                Unable to submit new market.
                                            </Typography>
                                        </Grid>
                                    </Grid>
                                )
                            }
                        </Fragment>
                    </Fragment>
                </Paper>
            </Container>
        </ThemeProvider>
    );
}


export default observer(MarketForm);