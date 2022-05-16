import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import LoginIcon from '@mui/icons-material/Login';
import { LoadingButton } from "@mui/lab";
import { Avatar, Box, Container, Grid, Link } from "@mui/material";
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect } from "react";
import { StoreContext } from '../../NewStores/StoreContext';



const LoginPage: NextPage = observer(() => {
    const stores = useContext(StoreContext)
    const router = useRouter();

    //component mount
    useEffect(() => {

    }, [])

    //component unmount
    useEffect(() => {
        return () => {
            
        }
    }, [])

    useEffect(() => {
        if(stores.authStore.auth.signedIn)
        {
            router.push("profile", undefined, { shallow: true })
        }
    }, [stores.authStore.auth.signedIn])

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        stores.authStore.auth.signIn();
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
                        value={stores.authStore.auth.email}
                        onChange={event => stores.authStore.auth.Email = event.target.value}
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
                        value={stores.authStore.auth.password}
                        onChange={event => stores.authStore.auth.Password = event.target.value}
                    />
                    <LoadingButton
                        type="submit"
                        loading={false}
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