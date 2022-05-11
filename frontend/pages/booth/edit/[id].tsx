import { TabContext, TabList, TabPanel } from "@mui/lab";
import { Box, Button, Container, Paper, Tab, TextField, Typography } from "@mui/material";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useCallback, useContext, useEffect, useState } from "react";
import { useErrorHandler } from "react-error-boundary";
import { NextPageAuth } from "../../../@types/NextAuthPage";
import BoothForm from "../../../components/BoothForm";
import { Booth } from "../../../NewStores/@DomainObjects/Booth";
import { StoreContext } from '../../../NewStores/StoreContext';


type Props = {
    id: string
}

const EditBoothPage: NextPageAuth<Props> = observer(() => {
    const stores = useContext(StoreContext);
    const handleError = useErrorHandler()
    const router = useRouter();
    const [selectedBooth, setSelectedBooth] = useState<Booth>(null)
    const [boothId, setBoothId] = useState<string>("");
    const [tabValue, setTabValue] = useState('1')


    const [file, setFile] = useState<File>()

    const saveFile = (e) => {
        setFile(e.target.files[0])
    }

    const uploadFile = useCallback(() => {
        selectedBooth.uploadBanner(file)
    }, [selectedBooth, file])

    //mount
    useEffect(() => {
        stores.itemCategoryStore.fetchCategories()
    }, [])

    useEffect(() => {
        if (!router.isReady) {
            return
        };
        var { id } = router.query
        setBoothId(id + "")
    }, [router.isReady]);

    useEffect(() => {
        if (selectedBooth == null) {
            if (!(boothId === "")) {
                flowResult(stores.boothStore.fetchBooth(boothId))
                    .then(res => {
                        setSelectedBooth(res)
                    })
                    .catch(error => {
                        handleError(error)
                    })
            }
        }
    }, [boothId, selectedBooth])

    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    return (
        <Container>
            <Paper elevation={1} sx={{ p: 2 }}>
                <Typography variant="h2">
                    Edit Boothpage
                </Typography>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="editBoothsTabs">
                            <Tab label="Booth Info" value="1" />
                            <Tab label="Images" value="2" />
                        </TabList>
                    </Box>                    
                    <TabPanel value="1">
                        {
                            selectedBooth != null && <BoothForm booth={selectedBooth} />
                        }
                    </TabPanel>
                    <TabPanel value="2">
                        {
                            (selectedBooth != null) &&
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

EditBoothPage.requireAuth = true;

export default EditBoothPage;