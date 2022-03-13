import { NextPage } from "next";
import { useRouter } from "next/router";
import React, { useEffect, useState } from "react";
import AddOrganiser from "../components/AddOrganiser";
import Sidebar from "../components/sidebar/Sidebar";

const menuItems = [
    {
        name: 'Organisers',
        iconClassName: "bi bi-person-bounding-box",
        to: '/Organiser',
        subMenu: [{ name: "Create New" }, { name: "List all" }]
    },
]

const TestPage: NextPage = () => {
    const router = useRouter()
    const [category, setCategory] = useState("")

    const handleSubjectSelect = (event) => {
        console.log("Subject select")
        console.log(event);
        console.log(event.currentTarget.id);
        setCategory('Organiser')
    }

    const handleItemSelect = (event) => {
        console.log("item select")
        console.log(event);
        console.log(event.currentTarget.id);
    }

    useEffect(() => {
        if(category === "Organiser")
        {
            router.push('/test', 'test/Organiser', { shallow: true })
        }
      }, [category])

    return (
        <div style={{display: 'grid', gridTemplateColumns: '300px auto'}}>
            <div>
                <Sidebar 
                    menuItems={menuItems} 
                    onItemSelect={handleItemSelect} 
                    onSubjectSelect={handleSubjectSelect}/>
            </div>
            <div>
                <AddOrganiser />
            </div>

        </div>
    )
}

export default TestPage;