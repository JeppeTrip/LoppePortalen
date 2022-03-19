import React, { FC, useContext, useEffect, useRef, useState } from "react";
import { OrganiserContext } from "../../stores/Organiser/OrganiserStore";
import { MarketContext } from "../../stores/Market/MarketStore";
import styles from "./styles.module.css";
import { IMarket } from "../../@types/Market";

type Props = {

}

const months = {
    0: "01",
    1: "02",
    2: "03",
    3: "04",
    4: "05",
    5: "06",
    6: "07",
    7: "08",
    8: "09",
    9: "10",
    10: "11",
    11: "12",
}

const MarketForm: FC<Props> = (props: Props) => {
    //TODO: Maybe move the status stuff out of the components themselves, I dunno.
    const organiserStore = useContext(OrganiserContext);
    const marketStore = useContext(MarketContext);

    const [market, setMarket] = useState<IMarket>({
        id: null,
        organiserId: null,
        name: "",
        startDate: new Date(),
        endDate: new Date(),
        description: ""
    })

    const handleUpdate = (key, value) => {
        console.log(key)
        console.log(value)
        if (key == "startDate" || key == "endDate") {
            if (value.length > 10) {
                setMarket(prevState => ({
                    ...prevState
                }));
            }
            else {
                setMarket(prevState => ({
                    ...prevState,
                    [key]: new Date(value)
                }));
            }
        }
    }



    const submitMarket = (event) => {

    }

    return (
        <>
            <div className={styles.container}>
                <div>
                    <select name="organisers" id="organisers">
                        {
                            organiserStore.organisers.map((organiser) => <option id={organiser.id+""} value={organiser.id}>{organiser.name}</option>)
                        }
                    </select>
                </div>
                <div>
                    <label className={styles.label} htmlFor="marketName">Market name:</label>
                    <input className={styles.input} type='text' id="marketName" name="marketName" value={market.name} onChange={e => handleUpdate("name", e.target.value)} />
                </div>

                <div className={styles.dateSection}>
                    <div>
                        <label className={styles.label} htmlFor="startDate">Start date:</label>
                        <input required className={styles.input} type='date' id="startDate" name="startDate" onChange={e => handleUpdate("startDate", e.target.value)}
                            value={market.startDate.toISOString().slice(0, 10)} />
                    </div>
                    <div>
                        <label className={styles.label} htmlFor="endDate">End date:</label>
                        <input required className={styles.input} id="endDate" name="endDate"
                            type='date' onChange={e => handleUpdate("endDate", e.target.value)} value={market.endDate.toISOString().slice(0, 10)} />
                    </div>
                </div>
                <div className={styles.editor}>
                    <textarea className={styles.textarea} value={market.description} onChange={e => handleUpdate("description", e.target.value)} />
                </div>
                <button id="submitMarket" onClick={submitMarket}>
                    Submit
                </button>
            </div >
        </>

    )
}

export default MarketForm;