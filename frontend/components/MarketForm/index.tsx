import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { observer } from 'mobx-react-lite';
import MarketDetailsForm from '../MarketDetailsForm';
import { FC, Fragment, useContext, useEffect, useState } from 'react';
import StallForm from '../StallForm';
import { StoreContext } from "../../stores/StoreContext";
import { LoadingButton } from "@mui/lab";
import SaveIcon from "@mui/icons-material/Save"
import { IMarket } from '../../@types/Market';
import { Grid } from '@mui/material';

type Props = {}

const steps = ['Market Information', 'Stalls'];

function getStepContent(step: number) {
    switch (step) {
        case 0:
            return <MarketDetailsForm />;
        case 1:
            return <StallForm />;
        default:
            throw new Error('Unknown step');
    }
}


const theme = createTheme();

const MarketForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const [activeStep, setActiveStep] = useState(0);

    useEffect(() => {
        console.log("marketform fires")
        stores.marketStore.resetNewMarket();
        stores.marketFormUiStore.resetState();
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
        var result = stores.marketStore.addNewMarket();
        console.log(result)
    }

    const loadingButton = () => <LoadingButton
        onClick={handleSubmit}
        loading={stores.marketFormUiStore.isSubmittingForm}
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

    return (
        <ThemeProvider theme={theme}>
            <Container component="main" maxWidth="md">
                <Paper variant="outlined" sx={{ p: { xs: 2, md: 3 } }}>
                    <Typography component="h1" variant="h4" align="center">
                        Create Market
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
                            <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                                {activeStep !== 0 && (
                                    <Button disabled={stores.marketFormUiStore.isSubmittingForm} onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
                                        Back
                                    </Button>
                                )}
                                {getButton()}
                            </Box>
                            {
                                //TODO: Make error handling waaay the fuck better.
                                stores.marketFormUiStore.showError &&
                                <Grid item xs={12}>
                                    <Typography variant="caption" color={"red"}>
                                        Could not submit.
                                    </Typography>
                                </Grid>
                            }
                        </Fragment>
                    </Fragment>
                </Paper>
            </Container>
        </ThemeProvider>
    );
}


export default observer(MarketForm);