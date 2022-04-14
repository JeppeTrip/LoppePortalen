import { Box, Container, Paper, Typography } from '@mui/material'


export default function Home() {

  const content = () => {
    return (
      <Paper elevation={1} sx={{ padding: "10px" }}>
        <Typography variant="h2" component="h2">
          Om Os
        </Typography>
        <Typography variant="h6">
          LoppePortalen gør det nemt, hurtigt og sikkert at oprette og afholde loppemarkeder.
        </Typography>
        <Typography variant="body1" component="h2">
          Vi tager os af alt det administrative, så du i stedet kan bruge tiden på det, der er sjovt, og gøre dit marked en god oplevelse for dine standholdere og gæster.

          Vi klarer booking af stande, sikrer betalinger, og hjælper med at skabe opmærksomhed for dit marked. Vi har også gjort det nemt at følge med i salg af stande og bevare overblikket på markedsdagen, så du nemt styr på alle stande, betalinger og kontaktinformationer.
        </Typography>
        <Typography variant="h6">
          Gratis?
        </Typography>
        <Typography variant="body1" component="h2">
          Det er gratis at arrangere et loppemarked på LoppePortalen. Vi lægger et lille gebyr til dine kunders standbetaling, som man kender det fra fx koncert- eller teaterbilletter. Du vælger altså helt selv, hvor meget en stand på dit marked skal koste. Her kan du se LoppePortalens gebyrer. Samtidigt vil vi gøre, hvad vi kan, for at alle markedsstande bliver solgt. Jo flere jo bedre - for standbookere, for markedsgæster, for dig og for os :)
        </Typography>
        <Typography variant="body1" component="h2">
          ...
        </Typography>
      </Paper>
    )
  }

  return (
    <Container sx={{ minHeight: "100%" }}>
      <Box sx={{ display: 'flex' }}>
        <Box component="main" sx={{ flexGrow: 1, p:0 }}>
          {
            content()
          }
        </Box>
      </Box>
    </Container>
  )
}
