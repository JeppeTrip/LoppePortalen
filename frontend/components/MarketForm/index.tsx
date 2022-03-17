import React, { FC, useContext } from "react";
import { OrganiserContext } from "../../stores/Organiser/OrganiserStore";

type Props = {

}

const AddOrganiser: FC<Props> = (props: Props) => {
    //TODO: Maybe move the status stuff out of the components themselves, I dunno.
    const store = useContext(OrganiserContext);



    return (
        <>

        </>
    )
}

export default AddOrganiser;