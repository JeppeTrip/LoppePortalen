import { Grid, TextField} from "@mui/material";

import { FC} from "react";

import { observer } from "mobx-react-lite";
import { Auth } from "../../NewStores/@DomainObjects/Auth";

type Props = {
    auth : Auth
}

const UserAccountForm: FC<Props> = (props: Props) => {  
    const handleEmailChange = (event) => {
        props.auth.email = event.target.value
    }

    const handlePasswordChange =(event) => {
        props.auth.password = event.target.value
    }

    return (
        <Grid container spacing={1}>
            <Grid item xs={12}>
                <TextField
                    value={props.auth.email}
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
                    value={props.auth.password}
                    onChange={event => handlePasswordChange(event)} />
            </Grid>
        </Grid >

    )
}

export default observer(UserAccountForm);