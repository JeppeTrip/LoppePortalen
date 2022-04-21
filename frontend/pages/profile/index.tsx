import CorporateFareIcon from '@mui/icons-material/CorporateFare';
import PersonIcon from '@mui/icons-material/Person';
import SellIcon from '@mui/icons-material/Sell';
import StoreIcon from '@mui/icons-material/Store';
import TableRestaurantIcon from '@mui/icons-material/TableRestaurant';
import { Container, Tooltip } from "@mui/material";
import Box from '@mui/material/Box';
import CssBaseline from '@mui/material/CssBaseline';
import Divider from '@mui/material/Divider';
import MuiDrawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import { CSSObject, styled, Theme } from '@mui/material/styles';
import { observer } from "mobx-react-lite";
import { useContext, useState } from "react";
import { NextPageAuth } from "../../@types/NextAuthPage";
import ProfileBoothInfo from '../../components/ProfileBoothInfo';
import ProfileMarketInfo from "../../components/ProfileMarketInfo";
import ProfileOrgInfo from "../../components/ProfileOrgInfo";
import ProfileSalesInfo from '../../components/ProfileSalesInfo';
import ProfileUserInfo from "../../components/ProfileUserInfo";
import { StoreContext } from "../../NewStores/StoreContext";



const drawerWidth = 240;

const openedMixin = (theme: Theme): CSSObject => ({
    width: drawerWidth,
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
    }),
    overflowX: 'hidden',
});

const closedMixin = (theme: Theme): CSSObject => ({
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: `calc(${theme.spacing(7)} + 1px)`,
    [theme.breakpoints.up('sm')]: {
        width: `calc(${theme.spacing(8)} + 1px)`,
    },
});

const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
}));

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme, open }) => ({
        width: drawerWidth,
        flexShrink: 0,
        whiteSpace: 'nowrap',
        boxSizing: 'border-box',
        ...(open && {
            ...openedMixin(theme),
            '& .MuiDrawer-paper': openedMixin(theme),
        }),
        ...(!open && {
            ...closedMixin(theme),
            '& .MuiDrawer-paper': closedMixin(theme),
        }),
    }),
);

const profileNavs = [
    {
        text: 'User Info',
        icon: <PersonIcon />
    },
    {
        text: 'Manage Organisations',
        icon: <CorporateFareIcon />
    },
    {
        text: 'Manage Markets',
        icon: <StoreIcon />
    },
    {
        text: 'Manage Salesprofiles',
        icon: <SellIcon />
    },
    {
        text: 'Manage Stalls',
        icon: <TableRestaurantIcon />
    }
]

const UserProfile: NextPageAuth = observer(() => {
    const stores = useContext(StoreContext)
    const [activeStep, setActiveStep] = useState(0);

    const getStepContent = (step: number) => {
        switch (step) {
            case 0:
                return <ProfileUserInfo user={stores.userStore.user} />;
            case 1:
                return <ProfileOrgInfo user={stores.userStore.user} />;
            case 2:
                return <ProfileMarketInfo user={stores.userStore.user} />;
            case 3:
                return <ProfileSalesInfo user={stores.userStore.user}/>;
            case 4: 
                return <ProfileBoothInfo user={stores.userStore.user}/>
            default:
                throw new Error('Unknown step');
        }
    }

    return (
        <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <Drawer
                variant="permanent"
                sx={{ zIndex: 0 }}>
                <DrawerHeader />
                <Divider />
                <List>
                    {profileNavs.map((nav) => (
                        <Tooltip title={nav.text} arrow placement="right">
                            <ListItemButton
                                key={nav.text}
                                sx={{
                                    minHeight: 48,
                                    justifyContent: 'center',
                                    px: 2.5,
                                }}
                                onClick={() => setActiveStep(profileNavs.indexOf(nav))}

                            >
                                <ListItemIcon
                                    sx={{
                                        minWidth: 0,
                                        mr: 'auto',
                                        justifyContent: 'center',
                                    }}
                                >
                                    {nav.icon}
                                </ListItemIcon>
                                <ListItemText primary={nav.text} sx={{ opacity: 0 }} />
                            </ListItemButton>
                        </Tooltip>

                    ))}
                </List>
            </Drawer>
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                <DrawerHeader />
                <Container>
                    {
                        stores.authStore.auth.signedIn && getStepContent(activeStep)
                    }
                </Container>
            </Box>
        </Box>
    )
})

UserProfile.requireAuth = true;

export default UserProfile;