import { Autocomplete, CircularProgress, Grid, TextField } from "@mui/material";
import { FC, useEffect, useState } from "react";

interface Postal {
    number: string;
    name: string;
}

type Props = {
    postalCode: string,
    city: string,
    onChange : (code : string, city : string ) => void
}

const RegionInput: FC<Props> = (props: Props) => {
    const [open, setOpen] = useState(false);
    const [postals, setPostals] = useState<readonly Postal[]>([]);
    const loadingPostal = open && postals.length === 0;

    useEffect(() => {
        let active = true;

        if (!loadingPostal) {
            return undefined;
        }

        (async () => {
            if (active) {
                fetch('https://api.dataforsyningen.dk/postnumre')
                    .then(response => response.json())
                    .then(rawData => rawData.map(x => {
                        const p = { number: x.nr, name: x.navn } as Postal
                        return p
                    }))
                    .then(data => {
                        setPostals([...data])
                    })
            }
        })();

        return () => {
            active = false;
        };
    }, [loadingPostal]);

    return (
        <Grid container spacing={2}>
            <Grid item xs={6}>
                <Autocomplete
                    id="postals-autocomplete"
                    open={open}
                    onOpen={() => {
                        setOpen(true);
                    }}
                    onClose={() => {
                        setOpen(false);
                    }}
                    isOptionEqualToValue={(option, value) => option.number === value.number}
                    getOptionLabel={(postal) => !postal.number || postal.number == null ? "" : postal.number }
                    options={postals}
                    loading={loadingPostal}
                    value={ {number: props.postalCode, name: props.city} as Postal}
                    onChange={(event, value) => {
                        console.log(value)
                        if(value == null)
                            props.onChange(null, null)
                        else
                            props.onChange(value.number, value.name)
                    }}
                    renderInput={(params) => (
                        <TextField
                            fullWidth
                            {...params}
                            label="Postal"
                            InputProps={{
                                ...params.InputProps,
                                endAdornment: (
                                    <>
                                        {loadingPostal ? <CircularProgress color="inherit" size={20} /> : null}
                                        {params.InputProps.endAdornment}
                                    </>
                                ),
                            }}
                        />
                    )}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    disabled
                    fullWidth
                    id="marketCity"
                    label="City"
                    variant="outlined"
                    value={props.city ? props.city : ""} />
            </Grid>
        </Grid>
    );
}


export default RegionInput;