import React, {FC} from 'react'
import styles from './styles.module.css'

type Props = {
    message? : string
}

const Banner : FC<Props> = ( props : Props) => {
    return(
        <div className={styles.Banner}>
            <div className={styles.img} />
        </div>
    )
}

export default Banner;