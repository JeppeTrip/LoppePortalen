import styles from './home.module.css';

export default function Home() {
  return (
    <span>
      <div className={styles.topContainer}>
        <div className={styles.topContent}>
          <div className={styles.topbar}>
              <div className={styles.topHeader}>
                Loppe Portalen
              </div>
          </div>
        </div>
      </div>
        
        <div className={styles.bodyGrid}>
          <div className={styles.filterContainer}>
            <div className={styles.filterMenu}>
              <h3>Sted</h3>
              <label for="start">Start date:</label>
              <input type="date" id="start" name="market-start" value={new Date().toISOString().slice(0, 10)}/>
              
              <label for="start">End date:</label>
              <input type="date" id="end" name="market-end" value={new Date().toISOString().slice(0, 10)}/>
              
              <h3>Kategorier</h3>
            </div>
          </div>

          <div className={styles.container}>
            <table className={styles.loppeTable} id='loppemarked table'>
              <thead>
                <tr>
                  <th>Sted</th>
                  <th>Dato</th>
                  <th>Information</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>
                <tr>
                  <td>Tisvildeleje</td>
                  <td>22/2/2222</td>
                  <td>Det er ofcourse placeholder</td>
                </tr>

              </tbody>
            </table>

          </div>
        </div>

    </span>
  )
}
