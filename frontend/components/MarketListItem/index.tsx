import { useRouter } from 'next/router';
import React, { FC, useEffect, useState } from 'react';
import DateDisplay from '../DateDisplay';
import styles from './styles.module.css';

type Props = {
    id: number,
    name: string,
    startDate: Date,
    endDate: Date
}

const MarketListItem: FC<Props> = (props: Props) => {
    const router = useRouter();
    
    const handleClick = (event) => {
        event.preventDefault()
        router.push(`/market/${props.id}`)
    }

    return (
        <li key={props.id} onClick={handleClick}>
            <div className={styles.container}>
                <h1>{props.name}</h1>
                <DateDisplay startDate={props.startDate} endDate={props.endDate} />
            </div>
        </li>
    )
}

export default MarketListItem