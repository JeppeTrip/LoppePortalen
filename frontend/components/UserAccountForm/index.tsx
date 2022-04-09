import { Grid, TextField} from "@mui/material";

import { FC, useContext} from "react";

import { observer } from "mobx-react-lite";
import { StoreContext } from "../../stores/StoreContext";
import { IUser } from "../../@types/User";

type Props = {
    user : IUser
}

const UserAccountForm: FC<Props> = (props: Props) => {  
    const handleEmailChange = (event) => {
        props.user.setEmail(event.target.value)
    }

    const handlePasswordChange =(event) => {
        props.user.setPassword(event.target.value)
    }

    return (
        <Grid container spacing={1}>
            <Grid item xs={12}>
                <TextField
                    value={props.user.email}
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
                    value={props.user.password}
                    onChange={event => handlePasswordChange(event)} />
            </Grid>
        </Grid >

    )
}

export default observer(UserAccountForm);