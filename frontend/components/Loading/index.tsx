import React, {FC} from 'react'
import styles from './styles.module.css'

const Loading : FC<React.ReactNode> = ({}) => {

    return(
        <>
            <div className={styles.container}>
                <i className="bi bi-cloud-arrow-down-fill"/>
                <h1>Fetching Market...</h1>
            </div>
        </>
    )
}

export default Loading;