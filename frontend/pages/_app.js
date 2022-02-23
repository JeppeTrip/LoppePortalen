import App from 'next/app'
import styles from './app.css'

function MyApp({ Component, pageProps }) {
    return <Component {...pageProps} />
  }
  
  export default MyApp