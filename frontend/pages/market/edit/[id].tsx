import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { IMarket } from "../../../@types/Market";
import Error from "../../../components/Error";
import Loading from "../../../components/Loading";
import { MarketClient } from "../../../stores/models";
import styles from './styles.module.css'

type Props = {
    id: string
}

const EditMarket: NextPage<Props> = () => {
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(false);

    const [isSubmitting, setIsSubmitting] = useState(false);
    const [submitError, setSubmitError] = useState(false);

    const [marketId, setMarketId] = useState<string>("");
    const [market, setMarket] = useState<IMarket>(null);
    const router = useRouter();

    useEffect(() => {
        if (!router.isReady) { return };
        var { id } = router.query
        setMarketId(id + "")

    }, [router.isReady]);

    useEffect(() => {
        if (marketId) {
            var client = new MarketClient();
            client.getMarketInstance(marketId + "").then(
                res => {
                    setMarket({
                        id: res.marketId,
                        organiserId: res.organiserId,
                        name: res.marketName,
                        startDate: new Date(res.startDate),
                        endDate: new Date(res.endDate),
                        description: res.description
                    });
                    setIsLoading(false);
                }).catch(error => {
                    setError(true);
                    setIsLoading(false);
                });
        }
    }, [marketId])

    const handleUpdate = (key, value) => {
        console.log(key)
        console.log(value)
        //handle the date times.
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
            //handle everything else.
        } else {
            setMarket(prevState => ({
                ...prevState,
                [key]: value
            }));
        }
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        setIsSubmitting(true);
        var client = new MarketClient();
        client.editMarketInstance({
            organiserId: market.organiserId,
            marketInstanceId: market.id,
            marketName: market.name,
            startDate: market.startDate,
            endDate: market.endDate,
            description: market.description
        }).then(res => {
            router.push(`/market/${res.marketInstanceId}`)
        }).catch(error => {
            setIsSubmitting(false);
            setSubmitError(true);
        })
    }

    return (
        isLoading ? <div style={{ gridColumnStart: "span 2" }}><Loading /></div> :
            error ? <Error message={"Ooops Something Went Wrong."} /> :
                <div className={styles.container}>
                    <div className={styles.content}>
                        <div>
                            <label className={styles.label} htmlFor="marketName">Market name:</label>
                            <input className={styles.input} type='text' id="marketName" name="marketName" value={market.name} onChange={e => handleUpdate("name", e.target.value)}/>
                        </div>
                        <div>
                            <label className={styles.label} htmlFor="startDate">Start date:</label>
                            <input required className={styles.input} type='date' id="startDate" name="startDate"
                                value={market.startDate.toISOString().slice(0, 10)} onChange={e => handleUpdate("startDate", e.target.value)}/>
                        </div>
                        <div>
                            <label className={styles.label} htmlFor="endDate">End date:</label>
                            <input required className={styles.input} id="endDate" name="endDate"
                                type='date' value={market.endDate.toISOString().slice(0, 10)} onChange={e => handleUpdate("endDate", e.target.value)} />
                        </div>

                        <label>Description:</label>
                        <textarea className={styles.textarea} value={market.description} onChange={e => handleUpdate("description", e.target.value)} />
                        {
                            isSubmitting ? <i style={{alignSelf: "center", justifySelf: "center"}}className="bi bi-cloud-arrow-up"></i> : <button onClick={handleSubmit}>Submit</button>
                        }
                        {
                            submitError && <div className={styles.errorMsg}>Something went wrong trying to submit your request, try again.</div>
                        }
                    </div>
                </div>
    )
}

export default EditMarket
