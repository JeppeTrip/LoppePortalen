import { Divider, List, Stack } from '@mui/material';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { observer } from 'mobx-react-lite';
import { useRouter } from 'next/router';
import { FC, useEffect } from 'react';
import { User } from '../../NewStores/@DomainObjects/User';
import MarketListItem from '../MarketListItem';

type Props = {
    user: User
}

const ProfileMarketInfo: FC<Props> = (props: Props) => {
    const router = useRouter();

    //Componentmounts
    useEffect(() => {
        props.user.fetchOwnedMarkets()
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
                props.user.markets.length == 0 ?
                    <Typography variant="subtitle2">
                        You have no markets. Start by creating a new one.
                    </Typography>
                    :
                    <List>
                        {
                            props.user.markets.map(market =>
                                <>
                                    {
                                        <MarketListItem Market={market} showControls={true} />
                                    }
                                </>
                            )
                        }
                    </List>
            }
        </Stack>
    );
}


export default observer(ProfileMarketInfo);