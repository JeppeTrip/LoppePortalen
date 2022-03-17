import React, { FC, useEffect, useState } from 'react';
import styles from './styles.module.css';
import profileImg from '../../../public/placeholder/company.png';

type Props = {
    id: number,
    name: string
}

const ListItem: FC<Props> = (props: Props) => {
    return (
        <li key={props.id}>
            <div className={styles.container}>
                <div className={styles.avatar}>
                    <img src='../../../public/placeholder/company.png' alt="User"/>
                </div>

                <div className={styles.content}>
                    <div className={styles.background} />

                    <div className={styles.name}>
                        <span>{props.name}</span>
                    </div>
                </div>
            </div>

        </li>
    )
}

export default ListItem;