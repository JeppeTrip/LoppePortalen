import React, { FC, useContext, useState } from "react";
import ReactDOM from 'react-dom';
import ReactQuill from "react-quill";
import { OrganiserContext } from "../../stores/Organiser/OrganiserStore";
import styles from "./styles.module.css";
import Script from 'next/script';

type Props = {

}

const MarketForm: FC<Props> = (props: Props) => {
    //TODO: Maybe move the status stuff out of the components themselves, I dunno.
    //const store = useContext(OrganiserContext);
    const [value, setValue] = useState('');



    return (
        <>
            <div className={styles.container}>
                <div>
                    <label className={styles.label} htmlFor="marketName">Market name:</label>
                    <input className={styles.input} type='text' id="marketName" name="marketName" />
                </div>

                <div className={styles.dateSection}>
                    <div>
                        <label className={styles.label} htmlFor="startDate">Start date:</label>
                        <input className={styles.input} type='date' id="startDate" name="startDate" />
                    </div>
                    <div>
                        <label className={styles.label} htmlFor="endDate">End date:</label>
                        <input className={styles.input} type='date' id="endDate" name="endDate" />
                    </div>
                </div>
                <div  className={styles.editor}>

                </div>

            </div>
        </>

    )
}

export default MarketForm;