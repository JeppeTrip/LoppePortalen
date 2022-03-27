import { Avatar, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, TextField } from "@mui/material";
import { NextPage } from "next";
import { observer } from "mobx-react-lite";
import styles from './styles.module.css'
import { RootStore } from "../../stores/RootStore";
import { useContext } from "react";
import {StoreContext} from '../../stores/StoreContext'
import OrganiserListItem from "../../components/OrganiserListItem";

const OrganiserListPage: NextPage = observer(() => {
    const stores = useContext(StoreContext);

    return (
        <>
            <List>
                {
                    stores.organiserStore.organisers.map(organiser => 
                    <>
                    {
                        <OrganiserListItem Organiser={organiser}/>
                    }
                    </>)
                }
            </List>
        </>
    )
})

export default OrganiserListPage;