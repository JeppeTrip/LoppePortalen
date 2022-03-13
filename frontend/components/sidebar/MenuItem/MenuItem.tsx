import React, { FC, useRef, useState } from 'react';
import styles from './styles.module.css';

type Props = {
    name: string,
    iconClassName: string,
    to: string,
    subMenus: any[],
    onItemSelect: (e : any) => void
    onSubjectSelect: (e: any) => void
}

const MenuItem: FC<Props> = (props: Props) => {
    const [expand, setExpand] = useState(false);

    const handleItemSelect = (e) => {
        e.stopPropagation();
        props.onItemSelect(e);
    }

    const handleSubjectSelect = (e) => {
        e.stopPropagation();
        setExpand(!expand)
        props.onSubjectSelect(e);

    }
    return (
        <li id={props.name} className={styles.listItem}  onClick={handleSubjectSelect}>
            <a className={styles.menuItem}>
                <div className={styles.menuIcon}>
                    <i className={props.iconClassName}></i>
                </div>
                <span>{props.name}</span>
            </a>
            {props.subMenus && props.subMenus.length > 0 ? (
                <ul className={(expand ? styles.subMenuActive : styles.subMenu)}>
                    {props.subMenus.map((menu, index) => 
                        <li key={index} id={menu.name} className={styles.listItem} onClick={handleItemSelect}>
                            <a >
                                {menu.name}
                            </a>
                        </li>
                    )}
                </ul>
            ) : null}
        </li>
    )
}

export default MenuItem;