import CorporateFareIcon from '@mui/icons-material/CorporateFare';
import DeckIcon from '@mui/icons-material/Deck';
import MenuIcon from '@mui/icons-material/Menu';
import StorefrontIcon from '@mui/icons-material/Storefront';
import TableRestaurantIcon from '@mui/icons-material/TableRestaurant';
import { AppBar, Button, IconButton, Menu, MenuItem, Toolbar, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { observer } from 'mobx-react-lite';
import { useRouter } from 'next/router';
import React, { FC, useContext } from 'react';
import { StoreContext } from '../../NewStores/StoreContext';
import AccountMenu from './AccountMenu';
import styles from './styles.module.css';

type Props = {

}

const menuItems = [
  {
    name: "Markets",
    icon: <DeckIcon />,
    path: '/market/'
  },
  {
    name: "Booths",
    icon: <TableRestaurantIcon />,
    path: '/booth/'
  },
  {
    name: "Organisers",
    icon: <CorporateFareIcon />,
    path: '/organiser/'
  },
  {
    name: "Merchants",
    icon: <StorefrontIcon />,
    path: '/merchant/'
  },
]

const TopBar: FC<Props> = (props: Props) => {
  const stores = useContext(StoreContext);
  const router = useRouter();

  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleOnClick = (path) => {
      if (router.isReady) {
          router.push(path, undefined, { shallow: true });
      }
  }


  return (
    <>
      <AppBar position="fixed">
        <Toolbar>
          <Typography
            className={styles.logo}
            variant="h6"
            noWrap
            component="div"
            sx={{ mr: 2, display: { xs: 'none', md: 'flex' } }}
            onClick={() => handleOnClick("/")}
          >
            Loppe Portalen
          </Typography>

          <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
            <IconButton
              size="large"
              aria-label="navigation properties"
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
                zIndex: 1,
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
            stores.authStore.auth.signedIn ? 
            (<AccountMenu />)
            : (<Button color="inherit" onClick={() => handleOnClick('/login')}>Login</Button>)
          }
          
          
        </Toolbar>
      </AppBar>
      <Toolbar />
    </>
  )
}

export default observer(TopBar);