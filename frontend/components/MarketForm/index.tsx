import React, { FC, useContext, useState } from "react";
import ReactQuill from "react-quill";
import { OrganiserContext } from "../../stores/Organiser/OrganiserStore";

type Props = {

}

const MarketForm: FC<Props> = (props: Props) => {
    //TODO: Maybe move the status stuff out of the components themselves, I dunno.
    //const store = useContext(OrganiserContext);
    const [value, setValue] = useState('');



    return (
        <>
            <ReactQuill theme="snow" value={value} onChange={setValue}/>
        </>
    )
}

export default MarketForm;