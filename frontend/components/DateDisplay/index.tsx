import React, { FC } from "react"
import styles from './styles.module.css'

type Props = {
    startDate: Date,
    endDate: Date
}
const DateDisplay: FC<Props> = (props: Props) => {
    return (
        
        <div className={styles.dateContainer}>
            <i className="bi bi-calendar-week-fill" />
            <p> {props.startDate.toDateString()} </p>
            <i className="bi bi-arrow-right" />
            <p>{props.endDate.toDateString()}</p>
        </div>
    )
};

export default DateDisplay;