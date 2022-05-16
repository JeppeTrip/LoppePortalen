import { TabContext, TabList, TabPanel } from "@mui/lab";
import { Container, Paper, Tab, Typography, TextField, Button } from "@mui/material";
import { Box } from '@mui/system';
import { flowResult } from 'mobx';
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useCallback, useContext, useEffect, useState } from "react";
import { useErrorHandler } from 'react-error-boundary';
import { ModelState } from '../../../@types/ModelState';
import { NextPageAuth } from "../../../@types/NextAuthPage";
import OrganiserContactsForm from '../../../components/OrganiserContactsForm';
import OrganiserForm from "../../../components/OrganiserForm";
import { Organiser } from '../../../NewStores/@DomainObjects/Organiser';
import { StoreContext } from "../../../NewStores/StoreContext";

type Props = {
    id: string
}

const EditOrganiserPage: NextPageAuth<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler()
    const [tabValue, setTabValue] = useState('1')
    const [organiserId, setOrganiserId] = useState<string>(undefined);
    const [selectedOrganiser, setSelectedOrganiser] = useState<Organiser>(new Organiser(null))
    const router = useRouter();

    const [file, setFile] = useState<File>()

    const saveFile = (e) => {
        setFile(e.target.files[0])
    }

    const uploadFile = useCallback(() => {
        selectedOrganiser.uploadBanner(file)
    }, [selectedOrganiser, file])

    const resetOrganiserState = useCallback(() => {
        if (selectedOrganiser != null)
            selectedOrganiser.state = ModelState.IDLE
    }, [selectedOrganiser]);

    useEffect(() => {
        if (!router.isReady) {
            return
        };
        var { id } = router.query
        setOrganiserId(id + "")
    }, [router.isReady]);

    /**
     * If you come here directly and edit organiser in the organiser store hasn't been set to anything
     * check if there is something in the current user. 
     * else go and check the backend
     * if nothing in the backend go to error page.
     */
    useEffect(() => {
        if (organiserId) {
            flowResult(stores.organiserStore.fetchOrganiser(parseInt(organiserId)))
                .then(organiser => {
                    organiser.state = ModelState.EDITING
                    setSelectedOrganiser(organiser)
                })
                .catch(error => {
                    handleError(error)
                });
        }
    }, [organiserId])

    useEffect(() => {
        if (selectedOrganiser.state === ModelState.IDLE)
            router.push(`/profile`, undefined, { shallow: true })
    }, [selectedOrganiser.state])

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    //Doesn't work fix when cleaning up (which should also be done fairly soon.)
    useEffect(() => {
        return () => {
            if (selectedOrganiser && selectedOrganiser != null && selectedOrganiser.state === ModelState.EDITING) {
                selectedOrganiser.state = ModelState.IDLE
            }
        }
    }, [])

    return (
        <Container
            style={{ paddingTop: "25px" }}
            maxWidth="sm">
            <Paper elevation={1}>
                <Typography variant="h2">
                    Edit Organiser
                </Typography>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="lab API tabs example">
                            <Tab label="Organiser Info" value="1" />
                            <Tab label="Contact Info" value="2" />
                            <Tab label="Images" value="3" />
                        </TabList>
                    </Box>
                    <TabPanel value="1">
                        {
                            (selectedOrganiser != null) && <OrganiserForm organiser={selectedOrganiser} />
                        }
                    </TabPanel>
                    <TabPanel value="2">
                        {
                            (selectedOrganiser != null) && <OrganiserContactsForm organiser={selectedOrganiser} />
                        }
                    </TabPanel>
                    <TabPanel value="3">
                        {
                            (selectedOrganiser != null) &&
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

EditOrganiserPage.requireAuth = true;

export default EditOrganiserPage;