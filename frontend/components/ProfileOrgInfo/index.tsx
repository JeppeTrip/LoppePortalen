import { Divider, List, Stack } from '@mui/material';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { useRouter } from 'next/router';
import { FC, useEffect } from 'react';
import { User } from '../../NewStores/@DomainObjects/User';
import OrganiserListItem from '../OrganiserListItem';

type Props = {
    user: User
}

const ProfileOrgInfo: FC<Props> = (props: Props) => {
    const router = useRouter();

    //Component mounts
    useEffect(() => {
        console.log("mount profile org info")
        console.log(props.user)
        if (!props.user.organisers || props.user.organisers.length === 0) {
            props.user.fetchOwnedOrganisers()
        }
    }, [])

    //Component unmounts
    useEffect(() => {
        return () => {

        }
    }, [])

    const handleOnNewOrganiser = () => {
        if (router.isReady) {
            router.push('/organiser/create', undefined, { shallow: true })
        }
    }

    return (
        <Stack spacing={1} >
            <Typography variant="h2">
                Your Organisations
            </Typography>
            <Divider />
            <Button
                onClick={() => handleOnNewOrganiser()}>
                New Organisation
            </Button>
            {
                (props.user.organisers == null) && (props.user.organisers.length == 0) ?
                    <Typography variant="subtitle2">
                        You have no organisers. Start by creating a new one.
                    </Typography>
                    :
                    <List>
                        {
                            props.user.organisers.map(organiser =>
                                <>
                                    {
                                        <OrganiserListItem Organiser={organiser} showEdit={true} />
                                    }
                                </>
                            )
                        }
                    </List>
            }
        </Stack>
    );
}


export default observer(ProfileOrgInfo);