import React, { FC, useContext, useEffect, useRef, useState } from "react";
import ReactDOM from 'react-dom';
import ReactQuill from "react-quill";
import { OrganiserContext } from "../../stores/Organiser/OrganiserStore";
import { MarketContext } from "../../stores/Market/MarketStore";
import styles from "./styles.module.css";
import Script from 'next/script';
import { start } from "repl";
import { CreateMarketRequest } from "../../stores/models";

type Props = {

}

const MarketForm: FC<Props> = (props: Props) => {
    //TODO: Maybe move the status stuff out of the components themselves, I dunno.
    const store = useContext(MarketContext);
    const [name, setName] = useState("")
    const [startDate, setStartDate] = useState(new Date())
    const [endDate, setEndDate] = useState(new Date())
    const [description, setDescription] = useState("")
    const [organiserId, setOrganiserId] = useState("")

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


    const handleUpdateName = (event) => {
        setName(event.target.value);
    };

    const handleUpdateStartDate = (event) => {
        setStartDate(new Date(event.target.value))
    };

    const handleUpdateEndDate = (event) => {
        setEndDate(new Date(event.target.value))
    };

    const handleUpdateDescription = (event) => {
        setDescription(event.target.value);
    };

    const handleUpdateOrganiserId = (event) => {
        setOrganiserId(event.target.value)
    }

    const submitMarket = (event) => {
        var market = {
            organiserId: Number(organiserId),
            marketName: name,
            description: description,
            startDate: startDate,
            endDate: endDate
        } as CreateMarketRequest;
        console.log(market)
        store.addMarket({
            market
        }).then((res) => {
            setName("");
            setStartDate(new Date());
            setEndDate(new Date());
            setDescription("");
            setOrganiserId("");
        })
    }

    return (
        <>
            <div className={styles.container}>
                <div>
                    <label className={styles.label} htmlFor="organiserId">Organiser Id:</label>
                    <input className={styles.input} type='text' id="organiserId" name="organiserId" value={organiserId} onChange={handleUpdateOrganiserId}/>
                </div>
                <div>
                    <label className={styles.label} htmlFor="marketName">Market name:</label>
                    <input className={styles.input} type='text' id="marketName" name="marketName" value={name} onChange={handleUpdateName}/>
                </div>

                <div className={styles.dateSection}>
                    <div>
                        <label className={styles.label} htmlFor="startDate">Start date:</label>
                        <input required className={styles.input} type='date' id="startDate" name="startDate" onChange={handleUpdateStartDate} value={startDate.toISOString().slice(0, 10)}/>
                    </div>
                    <div>
                        <label className={styles.label} htmlFor="endDate">End date:</label>
                        <input required className={styles.input} type='date' id="endDate" name="endDate" onChange={handleUpdateEndDate} value={endDate.toISOString().slice(0, 10)}/>
                    </div>
                </div>
                <div  className={styles.editor}>
                <textarea className={styles.textarea} value={description} onChange={handleUpdateDescription}/>
                </div>
                <button id="submitMarket" onClick={submitMarket}>
                    Submit
                </button>
            </div >
        </>

    )
}

export default MarketForm;