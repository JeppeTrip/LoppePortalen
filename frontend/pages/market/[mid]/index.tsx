import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { IMarket } from "../../../@types/Market";
import DateDisplay from "../../../components/DateDisplay";
import Loading from "../../../components/Loading";
import { MarketContext } from "../../../stores/Market/MarketStore";
import { MarketClient } from "../../../stores/models";
import styles from './../styles.module.css'

type Props = {
    mid: string
}

const MarketProfilePageID: NextPage<Props> = () => {
    const [isLoading, setIsLoading] = useState(true);

    const [marketId, setMarketId] = useState<string>("");
    const [market, setMarket] = useState<IMarket>(null);
    const router = useRouter();

    useEffect(() => {
        if (!router.isReady) { console.log("router is not ready."); return };
        console.log("router is ready.")
        var { mid } = router.query
        setMarketId(mid + "")
        console.log(mid);

    }, [router.isReady]);

    useEffect(() => {
        if (marketId) {
            var client = new MarketClient();
            client.getMarketInstance(marketId + "").then(
                res => setMarket({
                    id: res.marketId,
                    organiserId: res.organiserId,
                    name: res.marketName,
                    startDate: new Date(res.startDate),
                    endDate: new Date(res.endDate),
                    description: res.description
                }));
                setIsLoading(false);
        }
    }, [marketId])


    return (
        <div className={styles.profile}>
            <div className={styles.content}>
                {
                    isLoading ? <div style={{gridColumnStart: "span 2"}}><Loading /></div> :
                        <>
                            <div className={styles.informationContainer}>
                                {
                                    (market != undefined || market != null) &&
                                    <>
                                        {console.log(market)}
                                        <div className={styles.contentHeader}>
                                            <h1>
                                                {market.name}
                                            </h1>
                                            <DateDisplay startDate={market.startDate} endDate={market.endDate} />
                                        </div>

                                        <div className={styles.aboutInfo}>
                                            {market.description}
                                        </div>
                                    </>
                                }

                            </div>
                            <div className={styles.mapContainer}>
                                <div className={styles.mapPlaceholder} />
                            </div>
                        </>
                }

            </div>

        </div>
    )
}

export default MarketProfilePageID;