import { NextPage } from "next";
import { useRouter } from "next/router";
import React, { useEffect, useState } from "react";
import AddOrganiser from "../components/AddOrganiser";
import OrganiserList from "../components/OrganiserList";
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
    const [item, setItem] = useState("");

    const handleSubjectSelect = (event) => {
        if (category === event.currentTarget.id) {
            setCategory("")
        }
        else {
            setCategory(event.currentTarget.id)
        }


    }

    const handleItemSelect = (event) => {
        setItem(event.currentTarget.id)
    }

    useEffect(() => {
        if (category === "") {
            router.push('/test', '/test', { shallow: true })
        } else {
            router.push('/test', '/test/' + category + '/' + item, { shallow: true })
        }

    }, [category, item])

    return (
        <div style={{ display: 'grid', gridTemplateColumns: '300px auto' }}>
            <div>
                <Sidebar
                    menuItems={menuItems}
                    onItemSelect={handleItemSelect}
                    onSubjectSelect={handleSubjectSelect} />
            </div>
            <div style={{display: "flex", alignContent: "center", padding: "25px"}}>
                {
                    (category === "Organisers" && item === "Create New") && <AddOrganiser />
                }
                {
                    (category === "Organisers" && item === "List all") && <OrganiserList />
                }
            </div>

        </div>
    )
}

export default TestPage;