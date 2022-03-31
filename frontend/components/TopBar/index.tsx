import React, { FC, useContext, useEffect, useState } from 'react';
import { AppBar, IconButton, Toolbar, Typography } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import styles from './styles.module.css';
import { StoreContext } from '../../stores/StoreContext';
import { observer } from 'mobx-react-lite';

type Props = {

}

const TopBar: FC<Props> = (props: Props) => {
  const stores = useContext(StoreContext);

  return (
    <>
      <AppBar position="fixed"
        sx={{
          zIndex: 9999,
          width: { sm: stores.uiStateStore.isDrawerOpen ? `calc(100% - ${stores.uiStateStore.drawerWidth}px)` : `calc(100%)` },
          ml: { sm: `${stores.uiStateStore.drawerWidth}px` },
        }}>
        <Toolbar>
          <IconButton edge="start" color="inherit" aria-label="menu" sx={{ mr: 2 }} onClick={event => { event.preventDefault(); stores.uiStateStore.toggleDrawer() }}>
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" color="inherit" component="div">
            Loppe Portalen
          </Typography>
        </Toolbar>
      </AppBar>
      <Toolbar />
    </>
  )
}

export default observer(TopBar);