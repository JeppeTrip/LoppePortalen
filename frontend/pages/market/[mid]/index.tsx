import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { IMarket } from "../../../@types/Market";
import Banner from "../../../components/Banner";
import DateDisplay from "../../../components/DateDisplay";
import Error from "../../../components/Error";
import Loading from "../../../components/Loading";
import { MarketContext } from "../../../stores/Market/MarketStore";
import { MarketClient } from "../../../stores/models";
import styles from './../styles.module.css'

type Props = {
    mid: string
}

const MarketProfilePageID: NextPage<Props> = () => {
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(false);

    const [marketId, setMarketId] = useState<string>("");
    const [market, setMarket] = useState<IMarket>(null);
    const router = useRouter();

    useEffect(() => {
        if (!router.isReady) { return };
        var { mid } = router.query
        setMarketId(mid + "")

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

    const handleOnEdit = (event) => {
        event.preventDefault();
        router.push(`/market/edit/${marketId}`);
    }

    return (
        isLoading ? <div style={{ gridColumnStart: "span 2" }}><Loading /></div> :
            <div className={styles.profile}>
                <div className={styles.header}>
                    <div className={styles.coverPhoto}>
                        <Banner />
                    </div>
                    <div className={styles.headerInfo}>
                        <h1> {market.name} </h1>
                        <DateDisplay startDate={market.startDate} endDate={market.endDate} />
                        <button onClick={handleOnEdit}> Edit Market </button>
                    </div>
                </div>
                <div className={styles.contentArea} >
                    <div className={styles.aboutInfo}>
                        <h2>About</h2>
                        {market.description}
                    </div>
                    <div className={styles.mapContainer}>
                        <div className={styles.mapPlaceholder} />
                    </div>
                </div>
            </div>
    )
}

export default MarketProfilePageID;