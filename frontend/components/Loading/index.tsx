import React, {FC} from 'react'
import styles from './styles.module.css'

type Props = {
    message? : string
}

const Loading : FC<Props> = ( props : Props) => {
    return(
        <>
            <div className={styles.container}>
                <i className="bi bi-cloud-arrow-down-fill"/>
                <h1>{props.message ? props.message : "Loading Data"}</h1>
            </div>
        </>
    )
}

export default Loading;