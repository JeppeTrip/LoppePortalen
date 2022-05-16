import { Divider, List, Stack } from '@mui/material';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { useRouter } from 'next/router';
import { FC, useEffect } from 'react';
import { User } from '../../NewStores/@DomainObjects/User';
import BoothListItem from '../BoothListItem';

type Props = {
    user: User
}

const ProfileBoothInfo: FC<Props> = (props: Props) => {
    const router = useRouter();

    //Componentmounts
    useEffect(() => {
        props.user.fetchBooths()
    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {

        }
    }, [])

    return (
        <Stack spacing={1} >
            <Typography variant="h2">
                Manage Booths Here
            </Typography>
            <Divider />
            {
                props.user.booths.length === 0 ?
                    <Typography variant="subtitle2">
                        You have no booths at the moment. Book a stall at a market to begin with.
                    </Typography>
                    :
                    <List>
                        {
                            props.user.booths.map(x => <BoothListItem showEdit={true} booth={x}/>)
                        }
                    </List>
            }
        </Stack>
    );
}


export default observer(ProfileBoothInfo);