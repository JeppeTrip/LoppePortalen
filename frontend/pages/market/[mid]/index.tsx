import { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { IMarket } from "../../../@types/Market";
import DateDisplay from "../../../components/DateDisplay";
import { MarketContext } from "../../../stores/Market/MarketStore";
import styles from './../styles.module.css'

type Props = {
    mid: string
}

const MarketProfilePageID: NextPage<Props> = () => {
    const [market, setMarket] = useState<IMarket>();
    const store = useContext(MarketContext);
    const router = useRouter()
    const { mid } = router.query

    useEffect(() => {
        setMarket({
            id: 1,
            organiserId: 1,
            name: "Name of Market",
            startDate: new Date("2022-01-01"),
            endDate: new Date("2022-01-10"),
            description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc eget turpis ornare, suscipit tellus nec, fermentum justo. Praesent tempor luctus dolor at interdum. Nam sed auctor neque. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis pretium tortor. Vivamus urna nunc, ornare eu nulla quis, rhoncus feugiat nulla. Nulla eu tortor ut libero pulvinar consectetur. Ut rhoncus odio egestas nisi varius, vel sagittis nunc aliquam. Vestibulum placerat metus nec ligula egestas, vel elementum tortor ornare. Vivamus feugiat tincidunt augue non tempor. Donec convallis, nisl at auctor accumsan, eros tortor molestie mi, non maximus tellus magna et ante."
        })
    }, [])



    return (
        <div className={styles.profile}>
            <div className={styles.content}>
                <div className={styles.informationContainer}>
                    {
                        market != undefined &&
                        <>
                            <div className={styles.contentHeader}>
                                <h1>
                                    {mid+" "}
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
            </div>

        </div>
    )
}

export default MarketProfilePageID;