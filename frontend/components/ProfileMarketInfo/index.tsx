import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { FC, useContext, useEffect, useState } from 'react';
import { StoreContext } from "../../stores/StoreContext";
import { IUser } from '../../@types/User';
import { Autocomplete, Container, Divider, Grid, List, Paper, Stack, TextField } from '@mui/material';
import { DatePicker, LocalizationProvider } from '@mui/lab';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import OrganiserListItem from '../OrganiserListItem';
import { useRouter } from 'next/router';

type Props = {
    user: IUser
}

const ProfileMarketInfo: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    //Componentmounts
    useEffect(() => {
        stores.userStore.getUsersOrganisations(props.user);
    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {

        }
    }, [])

    const handleOnNewOrganiser = () => {
        if (router.isReady) {
            router.push('/market/create', undefined, { shallow: true })
        }
    }

    return (
        <Stack spacing={1} >
            <Typography variant="h2">
                Your Markets
            </Typography>
            <Divider />
            <Button
                onClick={() => handleOnNewOrganiser()}>
                New Market
            </Button>
            {
                props.user.organisations.length == 0 ?
                    <Typography variant="subtitle2">
                        You have no markets. Start by creating a new one.
                    </Typography>
                    :
                    <List>
                        {
                            /**markets here */
                        }
                    </List>
            }
        </Stack>
    );
}


export default observer(ProfileMarketInfo);