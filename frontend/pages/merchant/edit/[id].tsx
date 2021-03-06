import SaveIcon from "@mui/icons-material/Save";
import { LoadingButton, TabContext, TabList, TabPanel } from "@mui/lab";
import { Button, Container, Grid, Paper, Tab, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useCallback, useContext, useEffect, useState } from "react";
import { useErrorHandler } from "react-error-boundary";
import { ModelState } from '../../../@types/ModelState';
import { NextPageAuth } from "../../../@types/NextAuthPage";
import MerchantContactsForm from "../../../components/MerchantContactsForm";
import { Merchant } from '../../../NewStores/@DomainObjects/Merchant';
import { StoreContext } from '../../../NewStores/StoreContext';


type Props = {
    id: string
}

const EditMerchantPage: NextPageAuth<Props> = observer(() => {
    const handleError = useErrorHandler()
    const [tabValue, setTabValue] = useState('1')
    const [selectedMerchant, setSelectedMerchant] = useState<Merchant>(null)
    const stores = useContext(StoreContext);
    const [merchantId, setMerchantId] = useState<string>("");
    const router = useRouter();

    const [file, setFile] = useState<File>()

    const saveFile = (e) => {
        setFile(e.target.files[0])
    }

    const uploadFile = useCallback(() => {
        selectedMerchant.uploadBanner(file)
    }, [selectedMerchant, file])

    useEffect(() => {
        if (!router.isReady) {
            return
        };
        var { id } = router.query
        setMerchantId(id + "")
    }, [router.isReady]);

    /**
     * If selected market is empty in the stores search for it.
     */
    useEffect(() => {
        if (selectedMerchant == null) {
            if (!(merchantId === "")) {
                flowResult(stores.merchantStore.resolveMerchant(parseInt(merchantId)))
                    .then(res => {
                        setSelectedMerchant(res)
                    }).catch(error => {
                        handleError(error)
                    })
            }
        }
    }, [merchantId, selectedMerchant])

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    return (
        <Container >
            <Paper elevation={1}>
                <Typography variant="h2">
                    Edit Merchantprofile
                </Typography>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="edit-merchant-tabs">
                            <Tab label="Merchant Info" value="1" />
                            <Tab label="Contact Info" value="2" />
                            <Tab label="Images" value="3" />
                        </TabList>
                    </Box>
                    <TabPanel value="1">
                        {
                            selectedMerchant != null && (
                                <>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12}>
                                            <TextField
                                                fullWidth={true}
                                                id="merchantName"
                                                label="Name"
                                                variant="outlined"
                                                onChange={(event) => selectedMerchant.Name = event.target.value}
                                                value={selectedMerchant.name} />
                                        </Grid>

                                        <Grid item xs={12}>
                                            <TextField
                                                fullWidth
                                                id="outlined-multiline-static"
                                                label="Description"
                                                value={selectedMerchant.description}
                                                onChange={(event) => selectedMerchant.Description = event.target.value}
                                                multiline
                                                rows={10}
                                            />
                                        </Grid>
                                    </Grid>
                                    <Grid>
                                        <LoadingButton
                                            onClick={() => selectedMerchant.save()}
                                            loading={selectedMerchant.state === ModelState.SAVING}
                                            loadingPosition="start"
                                            startIcon={<SaveIcon />}
                                            variant="contained"
                                            sx={{ mt: 3, ml: 1 }}
                                        >
                                            Update
                                        </LoadingButton>
                                    </Grid>
                                    {
                                        selectedMerchant.state === ModelState.ERROR &&
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
                                </>
                            )
                        }

                    </TabPanel>
                    <TabPanel value="2">
                        {
                            (selectedMerchant != null) && <MerchantContactsForm merchant={selectedMerchant} />
                        }
                    </TabPanel>
                    <TabPanel value="3">
                        {
                            (selectedMerchant != null) &&
                            <>
                                <TextField
                                    id="outlined-full-width"
                                    label="Image Upload"
                                    name="upload-photo"
                                    type="file"
                                    fullWidth
                                    margin="normal"
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    variant="outlined"
                                    onChange={saveFile}
                                />
                                <Button onClick={uploadFile}> Upload Banner</Button>
                            </>
                        }
                    </TabPanel>
                </TabContext>
            </Paper>
        </Container>

    )
})

EditMerchantPage.requireAuth = true;

export default EditMerchantPage;