import Head from 'next/head'
import '../styles.css'
import React from 'react';
import Script from 'next/script';
import { Box, CssBaseline, Drawer } from '@mui/material'
import { StoreProvider } from '../stores/StoreContext';
import { RootStore } from '../stores/RootStore';
import DrawerMenu from '../components/DrawerMenu'
import { observer } from 'mobx-react-lite';
import TopBar from '../components/TopBar';

const rootStore = new RootStore();

function MyApp({ Component, pageProps }) {
  return (
    <>
      <Script src="https://connect.facebook.net/en_US/sdk.js" strategy="beforeInteractive" />
      <Head>
        <title>Loppe Portalen</title>
        <meta name="viewport" content="initial-scale=1, width=device-width" />
      </Head>


      <StoreProvider store={rootStore}>
        <CssBaseline />
        <TopBar />
        <Component {...pageProps} />
      </StoreProvider>

    </>


  )

}

// Only uncomment this method if you have blocking data requirements for
// every single page in your application. This disables the ability to
// perform automatic static optimization, causing every page in your app to
// be server-side rendered.
//
// MyApp.getInitialProps = async (appContext) => {
//   // calls page's `getInitialProps` and fills `appProps.pageProps`
//   const appProps = await App.getInitialProps(appContext);
//
//   return {...appProps}
// }

export default observer(MyApp)