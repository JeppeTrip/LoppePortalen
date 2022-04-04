import React, { FC, useContext, useEffect, useState } from 'react';
import { AppBar, Button, Container, Grid, IconButton, Menu, MenuItem, Toolbar, Typography } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { StoreContext } from '../../stores/StoreContext';
import { observer } from 'mobx-react-lite';
import StorefrontIcon from '@mui/icons-material/Storefront';
import CorporateFareIcon from '@mui/icons-material/CorporateFare';
import DeckIcon from '@mui/icons-material/Deck';
import TableRestaurantIcon from '@mui/icons-material/TableRestaurant';
import { Box } from '@mui/system';
import { useRouter } from 'next/router';

type Props = {

}

const menuItems = [
  {
    name: "Markets",
    icon: <DeckIcon />,
    path: '/market/'
  },
  {
    name: "Stalls",
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
    path: '/seller/'
  },
]

const TopBar: FC<Props> = (props: Props) => {
  const stores = useContext(StoreContext);
  const router = useRouter();

  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const handleOnClick = (path) => {
      if (router.isReady) {
          router.push(path, undefined, { shallow: true });
      }
  }

  const handleLogout = () => {
    stores.userStore.logout()
  }


  return (
    <>
      <AppBar position="fixed"
        sx={{
          zIndex: 9999,
          width: { sm: stores.uiStateStore.isDrawerOpen ? `calc(100% - ${stores.uiStateStore.drawerWidth}px)` : `calc(100%)` },
          ml: { sm: `${stores.uiStateStore.drawerWidth}px` },
        }}>
        <Toolbar>
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ mr: 2, display: { xs: 'none', md: 'flex' } }}
          >
            Loppe Portalen
          </Typography>

          <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'left',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'left',
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                zIndex: 9999,
                display: { xs: 'block', md: 'none' },
              }}
            >
              {menuItems.map((item) => (
                <MenuItem key={item.name} onClick={() => handleOnClick(item.path)}>
                  <Typography textAlign="center">{item.name}</Typography>
                </MenuItem>
              ))}
            </Menu>
          </Box>
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}
          >
            Loppe Portalen
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
            {menuItems.map((item) => (
              <Button
                key={item.name}
                onClick={() => handleOnClick(item.path)}
                sx={{ my: 2, color: 'white', display: 'block' }}
              >
                {item.name}
              </Button>
            ))}
          </Box>
          {
            stores.userStore.isLoggedIn ? <Button color="inherit" onClick={() => handleLogout()}>Logout</Button>
            : <Button color="inherit" onClick={() => handleOnClick('/login')}>Login</Button>
          }
          
          
        </Toolbar>
      </AppBar>
      <Toolbar />
    </>
  )
}

export default observer(TopBar);