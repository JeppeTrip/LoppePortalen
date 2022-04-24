import SaveIcon from "@mui/icons-material/Save";
import { LoadingButton, TabContext, TabList, TabPanel } from "@mui/lab";
import { Autocomplete, Container, Grid, Paper, Tab, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { flowResult } from "mobx";
import { observer } from "mobx-react-lite";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { ModelState } from '../../../@types/ModelState';
import { NextPageAuth } from "../../../@types/NextAuthPage";
import { Booth } from "../../../NewStores/@DomainObjects/Booth";
import { StoreContext } from '../../../NewStores/StoreContext';


type Props = {
    id: string
}

const EditBoothPage: NextPageAuth<Props> = observer(() => {
    const [tabValue, setTabValue] = useState('1')
    const [selectedBooth, setSelectedBooth] = useState<Booth>(null)
    const stores = useContext(StoreContext);
    const [boothId, setBoothId] = useState<string>("");
    const router = useRouter();

    //mount
    useEffect(() => {
        stores.itemCategoryStore.fetchCategories()
    }, [])

    //Unmount
    useEffect(() => () => {

    }, [])

    useEffect(() => {
        if (!router.isReady) {
            return
        };
        var { id } = router.query
        setBoothId(id + "")
    }, [router.isReady]);

    /**
     * If selected market is empty in the stores search for it.
     */
    useEffect(() => {
        if (selectedBooth == null) {
            if (!(boothId === "")) {
                flowResult(stores.boothStore.fetchBooth(boothId))
                    .then(res => {
                        setSelectedBooth(res)
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
                {
                    selectedBooth != null && (
                        <>
                            <Grid container spacing={2}>
                                <Grid item xs={12}>
                                    <TextField
                                        fullWidth={true}
                                        id="marketName"
                                        label="Name"
                                        variant="outlined"
                                        onChange={(event) => selectedBooth.name = event.target.value}
                                        value={selectedBooth.name} />
                                </Grid>

                                <Grid item xs={12}>
                                    <TextField
                                        fullWidth
                                        id="outlined-multiline-static"
                                        label="Description"
                                        value={selectedBooth.description}
                                        onChange={(event) => selectedBooth.description = event.target.value}
                                        multiline
                                        rows={10}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <Autocomplete
                                        onChange={(event,value) => selectedBooth.itemCategories = value}
                                        fullWidth
                                        multiple
                                        id="tags-standard"
                                        value={selectedBooth.itemCategories}
                                        loading={stores.itemCategoryStore.categories.length === 0}
                                        loadingText={"Fetching categories..."}
                                        options={
                                            stores.itemCategoryStore.categories
                                        }
                                    getOptionLabel={(option) => option}
                                    renderInput={(params) => (
                                        <TextField
                                            {...params}
                                            variant="standard"
                                            label="Item Categories"
                                            placeholder="Item Categories"
                                        />
                                    )}
                                    />
                                </Grid>
                            </Grid>
                            <Grid>
                                <LoadingButton
                                    onClick={() => selectedBooth.save()}
                                    loading={selectedBooth.state === ModelState.SAVING}
                                    loadingPosition="start"
                                    startIcon={<SaveIcon />}
                                    variant="contained"
                                    sx={{ mt: 3, ml: 1 }}
                                >
                                    Update
                                </LoadingButton>
                            </Grid>
                            {
                                selectedBooth.state === ModelState.ERROR &&
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
            </Paper>
        </Container>

    )
})

EditBoothPage.requireAuth = true;

export default EditBoothPage;