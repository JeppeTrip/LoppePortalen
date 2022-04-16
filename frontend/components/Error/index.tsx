import React, { FC } from 'react';
import styles from './styles.module.css';

type Props = {
    message : string;
}

const Error : FC<Props> = (props: Props) => {
    return(
        <>
            <div className={styles.container}>
                <i className="bi bi-cloud-hail"></i>
                <h1>{props.message}</h1>
            </div>
        </>
    )
}

export default Error;