import { Box, Container, List, Paper, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { NextPage } from "next";
import { useContext, useEffect } from "react";
import BoothFilter from "../../components/BoothFilter";
import BoothListItem from "../../components/BoothListItem";
import { StoreContext } from "../../NewStores/StoreContext";

const Booths: NextPage = observer(() => {
    const stores = useContext(StoreContext)

    useEffect(() => {
        stores.boothStore.fetchFilteredBooths();
    }, [])


    const content = () => {
        return (
            <Paper elevation={1}>
                {
                    stores.boothStore.booths.length == 0 ?
                        <Typography variant="subtitle2">
                            No booths found.
                        </Typography>
                        :
                        <List>
                            {
                                
                               stores.boothStore.booths.map(x => <BoothListItem showEdit={false} key={x.id} booth={x}/>)
                            }
                        </List>
                }

            </Paper>
        );
    }

    return (
        <Container>
            <Box sx={{ display: 'flex' }}>
                <BoothFilter />
                <Box component="main" sx={{ flexGrow: 1, p: 0 }}>
                    {
                        content()
                    }
                </Box>
            </Box>
        </Container>
    )
})

export default Booths;