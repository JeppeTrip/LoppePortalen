import { observer } from "mobx-react-lite";
import { FC, useContext, useEffect, useState } from "react";
import { IOrganiser } from "../../@types/Organiser";
import { ClientContext } from "../../services/Clients";
import { CreateOrganiserRequest } from "../../stores/models";
import { OrganiserContext } from "../../stores/Organiser/OrganiserStore";
import styles from "./styles.module.css";


type Props = {

}

const AddOrganiser: FC<Props> = (props: Props) => {
    const store = useContext(OrganiserContext);

    const [organiser, setOrganiser] = useState<IOrganiser>({
        id: null,
        name: "",
        description: "",
        street: "",
        streetNumber: "",
        appartment: "",
        postalCode: "",
        city: ""
    });

    const handleUpdate = (key, value) =>
        setOrganiser(prevState => ({
            ...prevState,
            [key]: value
        }));

    const onSubmit = (event) => {
        store.addOrganiser(organiser);
        setOrganiser({
            id: null,
            name: "",
            description: "",
            street: "",
            streetNumber: "",
            appartment: "",
            postalCode: "",
            city: ""
        });
    }

    return (
        <>
            <div>
                <h1> Create Organiser  </h1>
                <form className={styles.form}>
                    <fieldset>
                        <legend>General Information</legend>
                        <label className={styles.label} htmlFor="name">Name:</label>
                        <input className={styles.input} id="name" type="text" name="name" value={organiser.name} onChange={event => handleUpdate("name", event.target.value)} />
                    </fieldset>

                    <fieldset>
                        <legend>Address</legend>
                        <label className={styles.label} htmlFor="street"> Street: </label>
                        <input id="street" type="text" name="street" value={organiser.street} onChange={event => handleUpdate("street", event.target.value)} />

                        <label className={styles.label} htmlFor="StreetNumber"> Street Number: </label>
                        <input className={styles.input} id="streetNumber" type="text" name="streetNumber" value={organiser.streetNumber} onChange={event => handleUpdate("streetNumber", event.target.value)} />

                        <label className={styles.label} htmlFor="Appartment">Appartment: </label>
                        <input className={styles.input} id="Appartment" type="text" name="Appartment" value={organiser.appartment} onChange={event => handleUpdate("appartment", event.target.value)} />

                        <label className={styles.label} htmlFor="PostalCode"> Postal Code: </label>
                        <input className={styles.input} id="PostalCode" type="text" name="PostalCode" value={organiser.postalCode} onChange={event => handleUpdate("postalCode", event.target.value)} />

                        <label className={styles.label} htmlFor="City"> City: </label>
                        <input className={styles.input} id="City" type="ext" name="City" value={organiser.city} onChange={event => handleUpdate("city", event.target.value)} />
                    </fieldset>

                    <fieldset>
                        <legend>Contact Information</legend>
                        Empty for now.
                    </fieldset>

                    <input type="Button" value="Submit" onClick={onSubmit} readOnly={true} />

                </form>
            </div>
        </>
    )
}

export default AddOrganiser;