import { FC, useState } from "react";
import { OrganiserClient } from "../../stores/models";
import styles from "./styles.module.css";


type Props = {

}

const AddOrganiser: FC<Props> = (props: Props) => {
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [street, setStreet] = useState("");
    const [streetNumber, setStreetNumber] = useState("");
    const [appartment, setAppartment] = useState("");
    const [postalCode, setPostalCode] = useState("");
    const [city, setCity] = useState("");

    function handleChangeName(event){
        setName(event.target.value);
    }

    function handleChangeStreet(event){
        setStreet(event.target.value);
    }

    function handleChangeStreetNumber(event){
        setStreetNumber(event.target.value);
    }

    function handleChangeAppartment(event){
        setAppartment(event.target.value);
    }

    function handleChangePostalCode(event){
        setPostalCode(event.target.value);
    }

    function handleChangeCity(event){
        setCity(event.target.value);
    }

    function onSubmit(event)
    {
        event.stopPropagation();
        fetch("https://localhost:5001/api/Test", 
            {mode: 'no-cors'})
            .then((res)=>console.log(res)); 
    }

    return (
        <>
            <div>
                <form className={styles.form}>
                    <label htmlFor="name">Name:</label>
                    <input id="name" type="text" name="name" value={name} onChange={handleChangeName}/>

                    <label htmlFor="street"> Street: </label>
                    <input id="street" type="text" name="street" value={street} onChange={handleChangeStreet}/>

                    <label htmlFor="StreetNumber"> Street Number: </label>
                    <input id="StreetNumber" type="text" name="StreetNumber" value={streetNumber} onChange={handleChangeStreetNumber}/>

                    <label htmlFor="Appartment">Appartment: </label>
                    <input id="Appartment" type="text" name="Appartment" value={appartment} onChange={handleChangeAppartment}/>

                    <label htmlFor="PostalCode"> Postal Code: </label>
                    <input id="PostalCode" type="text" name="PostalCode" value={postalCode} onChange={handleChangePostalCode}/>

                    <label htmlFor="City"> City: </label>
                    <input id="City" type="ext" name="City" value={city} onChange={handleChangeCity}/>
                    <input type="Button" value="Submit" onClick={onSubmit} />
                </form>
            </div>
        </>
    )
}

export default AddOrganiser;