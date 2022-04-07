import { Avatar, Button, CircularProgress, Container, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, TextField } from "@mui/material";
import ImageIcon from '@mui/icons-material/Image'
import WorkIcon from '@mui/icons-material/Work'
import BeachAccessIcon from '@mui/icons-material/BeachAccess'
import { NextPage } from "next";
import TopBar from "../../../components/TopBar";
import { useContext, useEffect, useState } from "react";
import { StoreContext } from "../../../stores/StoreContext";
import { observer } from "mobx-react-lite";
import { DateTimePicker, LocalizationProvider } from "@mui/lab";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import styles from './styles.module.css'
import { IMarket } from "../../../@types/Market";
import MarketForm from "../../../components/MarketForm";

const CreateMarketPage: NextPage = observer(() => {

    const loading = () => {
        return (
            <CircularProgress />
        )
    }

    return (
        <>
            <Container
                style={{ paddingTop: "25px" }}
                maxWidth="sm">
                <TopBar />
                {
                    <MarketForm/>
                }
            </Container>
        </>
    )
})

export default CreateMarketPage;