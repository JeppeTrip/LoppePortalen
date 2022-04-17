import { CssBaseline, Toolbar } from '@mui/material';
import { observer } from 'mobx-react-lite';
import { AppProps } from 'next/app';
import dynamic from 'next/dynamic';
import Head from 'next/head';
import Script from 'next/script';
import React from 'react';
import { NextPageAuth } from '../@types/NextAuthPage';
import AuthGuard from '../components/AuthGuard';
import TopBar from '../components/TopBar';
import '../styles.css';

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

      <NewStoreProvider>
        <CssBaseline />
        <TopBar />
        <Toolbar /> {/* empty toolbar to push content into place */}
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