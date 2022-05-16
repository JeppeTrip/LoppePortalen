# LoppePortalen

## Backend 
The backend is in general developed in C#. 
I have by default used an underlying Postgres database. It is important the database used supports geographic location data. In the case of Postgres https://postgis.net/ is required.

NSWAG is also a required dependency as this is used to generate relevant clients. 

Docker is recommended but not required.

## Frontend
The frontend is built around Next.JS https://nextjs.org/learn/foundations/from-react-to-nextjs/getting-started-with-nextjs.

Uses Mobx https://mobx.js.org/README.html for statemanagement. 

There is also used https://mui.com/material-ui/getting-started/installation/ through out.