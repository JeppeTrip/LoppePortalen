import App from 'next/app'
import Head from 'next/head'
import { ClientStore } from '../services/Clients'
import { OrganiserStore } from '../stores/Organiser/OrganiserStore'
import { MarketStore } from '../stores/Market/MarketStore'
import '../styles.css'
import React from 'react';
import ReactDOM from 'react-dom';
import Script from 'next/script';

function MyApp({ Component, pageProps }) {
  return (
    <>
      <Script src="https://connect.facebook.net/en_US/sdk.js" strategy="beforeInteractive" />
      <Head>
        <title>Loppe Portalen</title>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.1/font/bootstrap-icons.css" />
        <link rel="stylesheet" href="https://unpkg.com/react-quill@1.3.3/dist/quill.snow.css" />

      </Head>

      <OrganiserStore>
        <MarketStore>
          <Component {...pageProps} />
        </MarketStore>
      </OrganiserStore>
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
//   return { ...appProps }
// }

export default MyApp