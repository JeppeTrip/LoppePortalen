import { Avatar, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, TextField } from "@mui/material";

import { FC, useContext, useEffect, useState } from "react";

import { observer } from "mobx-react-lite";
import { DateTimePicker, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import styles from './styles.module.css'
import { IMarket } from "../../@types/Market";
import { StoreContext } from "../../stores/StoreContext";
import { IOrganiser } from "../../@types/Organiser";

type Props = {}

const OrganiserForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const [newOrganiser, setNewOrganiser] = useState<IOrganiser>(
        {
            id: undefined,
            name: "",
            description: "",
            street: "",
            streetNumber: "",
            appartment: "",
            postalCode: "",
            city: ""
        }
    );

    const handleUpdate = (key, value) => {
        setNewOrganiser(prevState => ({
            ...prevState,
            [key]: value
        }));
    }

    const handleSubmit = (event) => {
        stores.organiserStore.addOrganiser(newOrganiser);
        setNewOrganiser({
            id: undefined,
            name: "",
            description: "",
            street: "",
            streetNumber: "",
            appartment: "",
            postalCode: "",
            city: ""
        });
    }

    return (
        <Grid container>
            
        </Grid>

    )
}

export default OrganiserForm;