import React, { FC, useContext, useEffect, useState } from 'react';
import { AppBar, IconButton, Toolbar, Typography } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import styles from './styles.module.css';

type Props = {

}

const TopBar: FC<Props> = (props: Props) => {

    return (
        <>
        <AppBar position="fixed">
          <Toolbar>
            <IconButton edge="start" color="inherit" aria-label="menu" sx={{ mr: 2 }}>
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

export default TopBar;