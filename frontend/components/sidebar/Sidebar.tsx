import React, { FC, useEffect, useState } from 'react';
import MenuItem from './MenuItem/MenuItem';
import styles from './styles.module.css';

type Props = {
    menuItems : any[],
    onSubjectSelect : (e : any) => void
    onItemSelect: (e : any) => void
    
};

const Sidebar: FC<Props> = (props: Props) => {
    const [inactive, setInactive] = useState(false);

    useEffect(() => {
        if(inactive){
            console.log(inactive);
            document.querySelectorAll('.subMenu').forEach(el => {
                console.log(el)
                el.classList.remove('Active');
            })
        }
    },[inactive])
    return (
        <>
            <div className={styles.sideBar}>
                <div className={styles.topSection}>
                    <div className={styles.logo}>
                        <img id={'logoSrc'} className={styles.logo} src={''} alt={'logo'} />
                    </div>
                    <div className={styles.toggleBtn} onClick={() => setInactive(!inactive)}>
                        {
                            inactive ? <i className="bi bi-arrow-right-square-fill"> </i> : <i className="bi bi-arrow-left-square-fill"></i>
                        }

                    </div>
                </div>

                <div className={styles.searchBar}>
                    <button className={styles.searchBtn}>
                        <i className='bi bi-search'> </i>
                    </button>
                    <input type='text' placeholder='Search' />
                </div>

                <div className={styles.divider} />

                <div className={styles.menu}>
                    <ul>
                        {
                            props.menuItems.map((menuItem, index) => 
                                <MenuItem
                                    key={index}
                                    iconClassName={menuItem.iconClassName}
                                    name={menuItem.name}
                                    to={menuItem.to}
                                    subMenus={menuItem.subMenu || []}
                                    onItemSelect={props.onItemSelect}
                                    onSubjectSelect={props.onSubjectSelect}
                                />
                            )
                        }
                    </ul>
                </div>

                <div className={styles.footer}>
                    <div className={styles.avatar}>
                        <img src="" alt="User" />
                    </div>
                    <div className={styles.userInfo}>
                        <h5>Jeppe Trip Kofoed</h5>
                        <p>trip.kofoed@outlook.com</p>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Sidebar