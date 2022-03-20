import { NextPage } from "next";
import styles from './styles.module.css'

const MarketProfilePage: NextPage = () => {

    return (
        <div className={styles.profile}>
            <div className={styles.content}>
                <div className={styles.informationContainer}>
                    <div className={styles.contentHeader}>
                        <h1>
                            Market Title
                        </h1>
                        <div className={styles.dateContainer}>
                            Jan 1 -> Jan 10
                        </div>
                    </div>

                    <div className={styles.aboutInfo}>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc eget turpis ornare, suscipit tellus nec, fermentum justo. Praesent tempor luctus dolor at interdum. Nam sed auctor neque. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis pretium tortor. Vivamus urna nunc, ornare eu nulla quis, rhoncus feugiat nulla. Nulla eu tortor ut libero pulvinar consectetur. Ut rhoncus odio egestas nisi varius, vel sagittis nunc aliquam. Vestibulum placerat metus nec ligula egestas, vel elementum tortor ornare. Vivamus feugiat tincidunt augue non tempor. Donec convallis, nisl at auctor accumsan, eros tortor molestie mi, non maximus tellus magna et ante. 
                    </div>
                </div>
                <div className={styles.mapContainer}>
                    <div className={styles.mapPlaceholder} />
                </div>
            </div>

        </div>
    )
}

export default MarketProfilePage;