import SaveIcon from "@mui/icons-material/Save";
import { LoadingButton } from "@mui/lab";
import { Autocomplete, Grid, TextField, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";
import { ModelState } from '../../@types/ModelState';
import { Booth } from "../../NewStores/@DomainObjects/Booth";
import { StoreContext } from "../../NewStores/StoreContext";


type Props = {
    booth : Booth
}

const BoothForm: FC<Props> = observer((props: Props) => {
    const stores = useContext(StoreContext);

    return (
        <>
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        fullWidth={true}
                        id="marketName"
                        label="Name"
                        variant="outlined"
                        onChange={(event) => props.booth.name = event.target.value}
                        value={props.booth.name} />
                </Grid>

                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        id="outlined-multiline-static"
                        label="Description"
                        value={props.booth.description}
                        onChange={(event) => props.booth.description = event.target.value}
                        multiline
                        rows={10}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Autocomplete
                        onChange={(event, value) => props.booth.itemCategories = value}
                        fullWidth
                        multiple
                        id="tags-standard"
                        value={props.booth.itemCategories}
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
                    onClick={() => props.booth.save()}
                    loading={props.booth.state === ModelState.SAVING}
                    loadingPosition="start"
                    startIcon={<SaveIcon />}
                    variant="contained"
                    sx={{ mt: 3, ml: 1 }}
                >
                    Update
                </LoadingButton>
            </Grid>
            {
                props.booth.state === ModelState.ERROR &&
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
})

export default BoothForm