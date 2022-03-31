import React, { FC, useContext, useEffect, useState } from 'react';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import { Drawer, ListItem } from '@mui/material';
import StorefrontIcon from '@mui/icons-material/Storefront';
import CorporateFareIcon from '@mui/icons-material/CorporateFare';
import InfoIcon from '@mui/icons-material/Info';
import DeckIcon from '@mui/icons-material/Deck';
import TableRestaurantIcon from '@mui/icons-material/TableRestaurant';
import { Box } from '@mui/system';
import { useRouter } from 'next/router';

type Props = {
    /**
     * Injected by the documentation to work in an iframe.
     * You won't need it on your project.
     */
    window?: () => Window;
    drawerWidth: number;
}

const menuItems = [
    {
        name: "Market",
        icon: <DeckIcon />,
        path: '/market/'
    },
    {
        name: "Stall",
        icon: <TableRestaurantIcon />,
        path: '/stall/'
    },
    {
        name: "Organisers",
        icon: <CorporateFareIcon />,
        path: '/organiser/'
    },
    {
        name: "Sellers",
        icon: <StorefrontIcon />,
        path: 'seller'
    },
    {
        name: "About",
        icon: <InfoIcon />,
        path: 'about'
    },

]

const DrawerMenu: FC<Props> = (props: Props) => {
    //TODO: this window prop is not necessary, remove this from the implementation.
    const { window } = props;
    const [mobileOpen, setMobileOpen] = React.useState(false);
    const drawerWidth = props.drawerWidth;

    const router = useRouter();

    const handleDrawerToggle = () => {
        setMobileOpen(!mobileOpen);
    };

    const handleOnClick = (path) => {
        if (router.isReady) {
            router.push(path, undefined, { shallow: true });
        }
    }

    const drawer = (
        <div>
            <Toolbar />
            <Divider />
            <List>
                {menuItems.map((item, index) => (
                    <ListItem button key={item.name} onClick={event => handleOnClick(item.path)}>
                        <ListItemIcon>
                            {item.icon}
                        </ListItemIcon>
                        <ListItemText primary={item.name} />
                    </ListItem>
                ))}
            </List>
        </div>
    );

    const container = window !== undefined ? () => window().document.body : undefined;

    return (
        <>
            {/* The implementation can be swapped with js to avoid SEO duplication of links. */}
            <Drawer
                container={container}
                variant="temporary"
                open={mobileOpen}
                onClose={handleDrawerToggle}
                ModalProps={{
                    keepMounted: true, // Better open performance on mobile.
                }}
                sx={{
                    display: { xs: 'block', sm: 'none' },
                    '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
                }}
            >
                {drawer}
            </Drawer>
            <Drawer
                variant="permanent"
                sx={{
                    display: { xs: 'none', sm: 'block' },
                    '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
                }}
                open
            >
                {drawer}
            </Drawer>
        </>
    )
}

export default DrawerMenu;