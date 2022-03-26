import { NextPage } from "next";
import Link from "next/link";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { IMarket } from "../../@types/Market";
import DateDisplay from "../../components/DateDisplay";
import Error from "../../components/Error";
import Loading from "../../components/Loading";
import MarketListItem from "../../components/MarketListItem";
import { MarketContext } from "../../stores/Markets/MarketStore";
import { MarketClient } from "../../stores/models";
import styles from './styles.module.css'

const MarketProfilePage: NextPage = () => {
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(false);

    const [markets, setMarkets] = useState<IMarket[]>([]);
    const store = useContext(MarketContext);

    useEffect(() => {
        var client = new MarketClient();
        client.getAllMarketInstances().then(res => {
            var results = res.map(m => {
                return (
                    {
                        id: m.marketId,
                        organiserId: m.organiserId,
                        name: m.marketName,
                        startDate: new Date(m.startDate),
                        endDate: new Date(m.endDate),
                        description: m.description
                    }
                )
            })

            setMarkets(results);
            setIsLoading(false);
        }).catch(error => {
            setError(true);
            setIsLoading(false);
        })
    }, [])

    return (
        <div className={styles.profile}>
            <div className={styles.content}>
                {
                    error ?
                        <div style={{ gridColumnStart: "span 2" }}>
                            <Error message={"Ooops Something Went Wrong."} />
                        </div>
                        : isLoading ? <div style={{ gridColumnStart: "span 2" }}><Loading /></div>
                            :
                            <ul style={{ gridColumnStart: "span 2" }}>
                                {
                                    markets.map(market =>
                                        <MarketListItem key={market.id} name={market.name} id={market.id} startDate={market.startDate} endDate={market.endDate} />
                                    )
                                }
                            </ul>
                }
            </div>

        </div>
    )
}

export default MarketProfilePage;