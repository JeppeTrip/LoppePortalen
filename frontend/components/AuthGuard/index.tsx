import { observer } from "mobx-react-lite";
import { useRouter } from "next/router"
import { useContext, useEffect } from "react"
import { StoreContext } from "../../NewStores/StoreContext";

type Props = {
 
}

const AuthGuard: React.FunctionComponent<Props> = props => {
  const authStore = useContext(StoreContext).authStore;
  const router = useRouter()

  useEffect(() => {
    console.log("AuthGuard")
    console.log(authStore.auth)
    if (!authStore.auth.initializing) {
      //auth is initialized and there is no user
      if (!authStore.auth.signedIn) {
        // remember the page that user tried to access
        //authStore.setRedirect(router.asPath)
        // redirect
        router.push('/login', undefined, { shallow: true });
      }

    }
  }, [authStore.auth.initializing, router, authStore.auth.signedIn])

  /* show loading indicator while the auth provider is still initializing */
  if (authStore.auth.initializing) {
    return <h1>Application Loading</h1>
  }

  // if auth initialized with a valid user show protected page
  if (!authStore.auth.initializing && authStore.auth.signedIn) {
    return <>{props.children}</>
  }

  /* otherwise don't return anything, will do a redirect from useEffect */
  return null
}

export default observer(AuthGuard);