import { Divider, List, Stack } from '@mui/material';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { useRouter } from 'next/router';
import { FC, useCallback, useEffect } from 'react';
import { User } from '../../NewStores/@DomainObjects/User';
import MerchantListItem from '../MerchantListItem';

type Props = {
    user: User
}

const ProfileSalesInfo: FC<Props> = (props: Props) => {
    const router = useRouter();

    //Componentmounts
    useEffect(() => {
        props.user.fetchMerchants()
    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {

        }
    }, [])

    const handleOnNewSalesProfile = useCallback(() => {
        if(router.isReady)
        {
            router.push("/merchant/create", undefined, {shallow: true})
        }
    },[router.isReady])

    return (
        <Stack spacing={1} >
            <Typography variant="h2">
                Manage Sales Profiles
            </Typography>
            <Divider />
            <Button
                onClick={() => handleOnNewSalesProfile()}>
                New Salesprofile
            </Button>
            {
                props.user.merchants.length === 0 ?
                    <Typography variant="subtitle2">
                        You have no sales profile. Start by making one.
                    </Typography>
                    :
                    <List>
                        {
                            props.user.merchants.map(x => <MerchantListItem showControls={true} merchant={x} />)
                        }
                    </List>
            }
        </Stack>
    );
}


export default observer(ProfileSalesInfo);