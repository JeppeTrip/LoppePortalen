import { Avatar, Box, Container, Grid, Link } from "@mui/material";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useContext } from "react";
import { StoreContext } from "../../stores/StoreContext";

import TextField from '@mui/material/TextField';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import { LoadingButton } from "@mui/lab";
import LoginIcon from '@mui/icons-material/Login';
import { useRouter } from "next/router";


const LoginPage: NextPage = observer(() => {
    const stores = useContext(StoreContext);
    const router = useRouter();

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        const email = data.get('email').toString();
        const password = data.get('password').toString();
        stores.userStore.login(email, password);
    };

    const handleClickSignup = (event) => {
        if (router.isReady) {
            router.push('signup', undefined, { shallow: true });
        };
    };

    return (
        <Container component="main" maxWidth="xs">
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Sign in
                </Typography>
                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email Address"
                        name="email"
                        autoComplete="email"
                        autoFocus
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Password"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                    />
                    {
                        /*
                                    <FormControlLabel
                      control={<Checkbox value="remember" color="primary" />}
                      label="Remember me"
                    />
                        */
                    }
                    <LoadingButton
                        type="submit"
                        loading={stores.userStore.isLoggingIn}
                        loadingPosition="start"
                        startIcon={<LoginIcon />}
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                        fullWidth
                    >
                        Sign In
                    </LoadingButton>
                </Box>
            </Box>
            <Grid container>
                {
                    /*
                                <Grid item xs>
                                    <Link href="#" variant="body2">
                                        Forgot password?
                                    </Link>
                                </Grid>
                                */
                }
                <Grid item>
                    <Link
                        component={"button"}
                        variant="body2"
                        onClick={(event) => { handleClickSignup(event) }}>
                        {"Don't have an account? Sign Up"}
                    </Link>
                </Grid>
            </Grid>
        </Container>
    );
})

export default LoginPage;