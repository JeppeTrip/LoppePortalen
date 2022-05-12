import { Autocomplete, Grid, TextField } from "@mui/material";
import throttle from 'lodash/throttle';
import { FC, useEffect, useMemo, useState } from "react";
import { Location } from "../../@types/Location";

type Props = {
    value : Location
    onChange: (location : Location) => void
}

const RegionInput: FC<Props> = (props: Props) => {
    const [open, setOpen] = useState(false);
    const [locations, setLocations] = useState<readonly Location[]>([]);
    const [queryString, setQueryString] = useState<string>("")
    const loading = open && locations.length === 0;


    const fetchOptions = useMemo(
        () =>
            throttle(
                (
                    request: { input: string },
                    callback: (results?: readonly Location[]) => void,
                ) => {
                    fetch(`https://api.dataforsyningen.dk/adresser/autocomplete?struktur=mini${request.input.length === 0 ? "" : `&q=${request.input}`}`)
                        .then(response => response.json())
                        .then(rawData => rawData.map(loc => {
                            const street = loc.adresse.vejnavn
                            const husnr = loc.adresse.husnr
                            const floor = loc.adresse.etage == null ? "" : `, ${loc.adresse.etage}.`
                            const door = loc.adresse.dør == null ? "" : ` ${loc.adresse.dør}`
                            const location = {
                                text: loc.tekst,
                                address: `${street} ${husnr}${floor}${door}`,
                                postalCode: loc.adresse.postnr,
                                city: loc.adresse.postnrnavn,
                                x: loc.adresse.x,
                                y: loc.adresse.y
                            } as Location

                            return location
                        })).
                        then(locs => callback(locs))
                },
                200,
            ),
        [],
    );

    useEffect(() => {
        console.log("use effect")
        let active = true;

        if (queryString === '') {
            setLocations(props.value ? [props.value] : []);
            return undefined;
        }

        fetchOptions({ input: queryString }, (results?: readonly Location[]) => {
            console.log("fetch options callback?")
            console.log(results)
            if (active) {
                console.log("active?")
                let newOptions: readonly Location[] = [];

                if (props.value) {
                    newOptions = [props.value];
                }

                if (results) {
                    newOptions = [...newOptions, ...results];
                }

                setLocations(newOptions);
            }
        })

        return () => {
            active = false;
        };
    }, [props.value, queryString, fetchOptions]);

    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <Autocomplete
                    id="address-autocomplete"
                    getOptionLabel={(location) => {
                        return location.text
                    }}
                    filterOptions={(x) => x}
                    options={locations}
                    autoComplete
                    includeInputInList
                    filterSelectedOptions
                    isOptionEqualToValue={(option, value) => option.address === value.address}
                    loading={loading}
                    value={props.value}
                    onChange={(event: any, newValue: Location | null) => {
                        setLocations(newValue ? [newValue, ...locations] : locations);
                        props.onChange(newValue);
                    }}
                    onInputChange={(event, newInputValue) => {
                        setQueryString(newInputValue);
                    }}
                    renderInput={(params) => (
                        <TextField {...params} label="Add a location" fullWidth />
                      )}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    disabled
                    fullWidth
                    id="postalCode"
                    label="City"
                    variant="outlined"
                    value={props.value ? props.value.postalCode : ""} />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    disabled
                    fullWidth
                    id="city"
                    label="City"
                    variant="outlined"
                    value={props.value ? props.value.city : ""} />
            </Grid>
        </Grid>
    );
}


export default RegionInput;