import { NextPage } from "next";
import { observer } from "mobx-react-lite";

import * as React from 'react';
import { styled, Theme, CSSObject } from '@mui/material/styles';
import Box from '@mui/material/Box';
import MuiDrawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import CssBaseline from '@mui/material/CssBaseline';
import Divider from '@mui/material/Divider';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import PersonIcon from '@mui/icons-material/Person';
import CorporateFareIcon from '@mui/icons-material/CorporateFare';
import SellIcon from '@mui/icons-material/Sell';
import { Container, Tooltip } from "@mui/material";
import { StoreContext } from "../../stores/StoreContext";
import ProfileUserInfo from "../../components/ProfileUserInfo";
import { NextPageAuth } from "../../@types/NextAuthPage";

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
        text: 'Manage Salesprofiles',
        icon: <SellIcon />
    }
]

const UserProfile: NextPageAuth = observer(() => {
    const stores = React.useContext(StoreContext);
    const [open, setOpen] = React.useState(false);

    //Don't know if this is the way to do this.
    React.useEffect(() => {
        if (stores.authStore.signedIn) {
            if (stores.userStore.currentUser == null || stores.userStore.currentUser.id == "") {
                stores.userStore.getCurrentUser()
            }
        }
    }, [])

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
                                    justifyContent: open ? 'initial' : 'center',
                                    px: 2.5,
                                }}
                            >
                                <ListItemIcon
                                    sx={{
                                        minWidth: 0,
                                        mr: open ? 3 : 'auto',
                                        justifyContent: 'center',
                                    }}
                                >
                                    {nav.icon}
                                </ListItemIcon>
                                <ListItemText primary={nav.text} sx={{ opacity: open ? 1 : 0 }} />
                            </ListItemButton>
                        </Tooltip>

                    ))}
                </List>
            </Drawer>
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                <DrawerHeader />
                <Container>
                    <ProfileUserInfo user={stores.userStore.currentUser} />
                </Container>
            </Box>
        </Box>
    )
})

UserProfile.requireAuth = true;

export default UserProfile;