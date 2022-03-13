import App from 'next/app'
import Head from 'next/head'
import { ClientStore } from '../services/Clients'
import { OrganiserStore } from '../stores/Organiser/OrganiserStore'
import '../styles.css'

function MyApp({ Component, pageProps }) {
  return (
    <>
    <Head>
      <title>Loppe Portalen</title>
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.1/font/bootstrap-icons.css"/>
    </Head>
      <ClientStore>
        <OrganiserStore>
          <Component {...pageProps} />
        </OrganiserStore>
      </ClientStore>
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