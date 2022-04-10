import { observer } from "mobx-react-lite";
import { useRouter } from "next/router"
import { useContext, useEffect } from "react"
import { StoreContext } from "../../stores/StoreContext"

type Props = {}

const AuthGuard: React.FunctionComponent<Props> = props => {
  const authStore = useContext(StoreContext).authStore;
  const router = useRouter()

  useEffect(() => {
    if (!authStore.initializing) {
      //auth is initialized and there is no user
      if (!authStore.signedIn) {
        // remember the page that user tried to access
        authStore.setRedirect(router.asPath)
        // redirect
        router.push('/login', undefined, { shallow: true });
      }

    }
  }, [authStore.initializing, router, authStore.signedIn])

  /* show loading indicator while the auth provider is still initializing */
  if (authStore.initializing) {
    return <h1>Application Loading</h1>
  }

  // if auth initialized with a valid user show protected page
  if (!authStore.initializing && authStore.signedIn) {
    return <>{props.children}</>
  }

  /* otherwise don't return anything, will do a redirect from useEffect */
  return null
}

export default observer(AuthGuard);