import Head from 'next/head'
import '../styles.css'
import React, { useEffect, useState } from 'react';
import Script from 'next/script';
import { Box, CssBaseline, Drawer, Stack } from '@mui/material'
import { StoreProvider } from '../stores/StoreContext';
import { RootStore } from '../stores/RootStore';
import { observer } from 'mobx-react-lite';
import TopBar from '../components/TopBar';
import { NextPage } from 'next';
import { AppProps } from 'next/app';
import AuthGuard from '../components/AuthGuard';
import { NextPageAuth } from '../@types/NextAuthPage';
import dynamic from 'next/dynamic'

const oldRootStore = new RootStore();

function MyApp(props: AppProps) {
  const {
    Component,
    pageProps,
  }: { Component: NextPageAuth; pageProps: any } = props
  const NewStoreProvider = dynamic(() => import('../NewStores/StoreContext').then(prov => prov.StoreProvider), {
    ssr: false,
  });

  return (
    <>
      <Script src="https://connect.facebook.net/en_US/sdk.js" strategy="beforeInteractive" />
      <Head>
        <title>Loppe Portalen</title>
        <meta name="viewport" content="initial-scale=1, width=device-width" />
      </Head>


      <StoreProvider store={oldRootStore}>
        <NewStoreProvider>
          <CssBaseline />
          <TopBar />
          {
            Component.requireAuth ? (
              <AuthGuard>
                <Component {...pageProps} />
              </AuthGuard>
            ) : (
              <Component {...pageProps} />
            )
          }
        </NewStoreProvider>
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