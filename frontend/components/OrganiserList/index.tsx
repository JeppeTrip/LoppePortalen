import React, { FC, useContext, useEffect, useState } from 'react';
import { OrganiserContext } from '../../stores/Organiser/OrganiserStore';
import ListItem from './ListItem/ListItem';
import styles from './styles.module.css';

type Props = {

}

const OrganiserList: FC<Props> = (props: Props) => {
    const store = useContext(OrganiserContext);
    const [organisers, setOrganisers] = useState([]);
   
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [hasNextPage, setHasNextPage] = useState(true);
    
    const [hasReachedBottom, setHasReachedBottom] = useState(true);

    const [isLoading, setIsLoading] = useState(false);


    useEffect(() => {
        var element = document.getElementById('organiserList')
        console.log(element.scrollHeight);
        console.log(element.scrollTop)
        console.log(element.clientHeight);
        setHasReachedBottom(element.scrollHeight - element.scrollTop === element.clientHeight)

    }, [organisers])

    useEffect(() => {
        console.log('Load')

        if (hasReachedBottom && hasNextPage) {

            setIsLoading(true)
            console.log('is loading')
            store.getOrganisers(pageNumber, pageSize)
                .then(res => {
                    setOrganisers(organisers.concat(res.organisers.items));
                    setHasNextPage(res.organisers.hasNextPage);
                    if(res.organisers.hasNextPage){
                        setPageNumber(pageNumber+1);
                    }
                }).then(() => setIsLoading(false));
            setHasReachedBottom(false);
        }
    }, [hasReachedBottom])

    const handleScroll = (event) => {
        setHasReachedBottom(event.target.scrollHeight - event.target.scrollTop === event.target.clientHeight)
    }

    return (
        <div  className={styles.ol} >
            <ul id={'organiserList'} onScroll={handleScroll}>
                {
                    organisers.map(o =>
                        <ListItem id={o.id} name={o.name} />
                    )
                }
            </ul>
        </div>
    )
}

export default OrganiserList;