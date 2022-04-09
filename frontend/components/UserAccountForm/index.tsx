import { Grid, TextField} from "@mui/material";

import { FC, useContext} from "react";

import { observer } from "mobx-react-lite";
import { StoreContext } from "../../stores/StoreContext";

type Props = {}

const UserAccountForm: FC<Props> = (props: Props) => {
    const stores = useContext(StoreContext);
    
    const handleEmailChange = (event) => {
        stores.userStore.newUser.setEmail(event.target.value)
    }

    const handlePasswordChange =(event) => {
        stores.userStore.newUser.setPassword(event.target.value)
    }

    return (
        <Grid container spacing={1}>
            <Grid item xs={12}>
                <TextField
                    value={stores.userStore.newUser.email}
                    onChange={event => handleEmailChange(event)}
                    required
                    fullWidth
                    id="email"
                    label="Email Address"
                    name="email"
                    autoComplete="email"
                    autoFocus />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    required
                    fullWidth
                    name="password"
                    label="Password"
                    type="password"
                    id="password"
                    autoComplete="current-password"
                    value={stores.userStore.newUser.password}
                    onChange={event => handlePasswordChange(event)} />
            </Grid>
        </Grid >

    )
}

export default observer(UserAccountForm);