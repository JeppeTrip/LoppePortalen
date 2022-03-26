import App from 'next/app'
import Head from 'next/head'
import '../styles.css'
import React from 'react';
import ReactDOM from 'react-dom';
import Script from 'next/script';
import { CssBaseline } from '@mui/material'
import { createContext } from 'vm'
import { StoreProvider } from '../stores/StoreContext';
import { RootStore } from '../stores/RootStore';

const rootStore = new RootStore();

function MyApp({ Component, pageProps }) {
  return (
    <>
      <Script src="https://connect.facebook.net/en_US/sdk.js" strategy="beforeInteractive" />
      <Head>
        <title>Loppe Portalen</title>
        <link
          rel="stylesheet"
          href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap"
        />
        <link
          rel="stylesheet"
          href="https://fonts.googleapis.com/icon?family=Material+Icons"
        />
        <meta name="viewport" content="initial-scale=1, width=device-width" />

        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.1/font/bootstrap-icons.css" />
        <link rel="stylesheet" href="https://unpkg.com/react-quill@1.3.3/dist/quill.snow.css" />
        <link rel="preconnect" href="https://fonts.googleapis.com" />
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
        <link href="https://fonts.googleapis.com/css2?family=Quicksand:wght@300;600&display=swap" rel="stylesheet" />
      </Head>


        <StoreProvider store={rootStore}>
          <CssBaseline />
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

export default MyApp