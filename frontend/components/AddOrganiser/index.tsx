import { observer } from "mobx-react-lite";
import { FC, useContext, useEffect, useState } from "react";
import { ClientContext } from "../../services/Clients";
import { CreateOrganiserRequest } from "../../stores/models";
import { OrganiserContext } from "../../stores/Organiser/OrganiserStore";
import styles from "./styles.module.css";


type Props = {

}

const AddOrganiser: FC<Props> = (props: Props) => {
    //TODO: Maybe move the status stuff out of the components themselves, I dunno.
    const clients = useContext(ClientContext);
    const store = useContext(OrganiserContext);

    const [organiser, setOrganiser] = useState({
        name: "",
        street: "",
        number: "",
        appartment: "",
        postalCode: "",
        city: ""
    } as CreateOrganiserRequest);
    const [state, setState] = useState({
        loading: false,
        error: false,
        success: false
    });

    const handleUpdate = (key, value) =>
        setOrganiser(prevState => ({
            ...prevState,
            [key]: value
        }));

    const onSubmit = (event) => {
        setState(
            prevState => ({
                ...prevState,
                success: false,
                error: false,
                loading: true
            }));
        store.addOrganiser(organiser)
            .then(res => {
                setState(
                    prevState => ({
                        ...prevState,
                        loading: false
                    }));
                if (!(res.id == null || res.id === "")) {
                    setState(
                        prevState => ({
                            ...prevState,
                            success: true
                        }));
                    setOrganiser({
                        name: "",
                        street: "",
                        number: "",
                        appartment: "",
                        postalCode: "",
                        city: ""
                    })
                } else {
                    setState(
                        prevState => ({
                            ...prevState,
                            error: false
                        }));
                }
            })
            .catch(e => {
                setState(
                    prevState => ({
                        ...prevState,
                        loading: false,
                        error: true
                    }));
            });
    }

    return (
        <>
            <div>
                <div>
                    loading: {state.loading + ""}
                </div>
                <div>error: {state.error + ""}</div>
                <div>success: {state.success + ""}</div>

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
                        <input className={styles.input} id="StreetNumber" type="text" name="StreetNumber" value={organiser.number} onChange={event => handleUpdate("number", event.target.value)} />

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