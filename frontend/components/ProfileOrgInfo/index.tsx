import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { FC, useContext, useEffect } from 'react';
import { StoreContext } from "../../stores/StoreContext";
import { Divider, List, Stack } from '@mui/material';
import OrganiserListItem from '../OrganiserListItem';
import { useRouter } from 'next/router';
import { User } from '../../NewStores/@DomainObjects/User';

type Props = {
    user: User
}

const ProfileOrgInfo: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    //Component mounts
    useEffect(() => {
        console.log("mount profile org info")
        console.log(props.user)
        if (!props.user.organisers || props.user.organisers.length === 0) {
            props.user.getOrganisers()
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