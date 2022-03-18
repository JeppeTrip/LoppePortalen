import { NextPage } from "next";
import { useRouter } from "next/router";
import React, { useEffect, useState } from "react";
import AddOrganiser from "../components/AddOrganiser";
import MarketForm from "../components/MarketForm";
import OrganiserList from "../components/OrganiserList";
import Sidebar from "../components/sidebar/Sidebar";

const menuItems = [
    {
        name: 'Organisers',
        iconClassName: "bi bi-person-bounding-box",
        to: '/Organiser',
        subMenu: [{ name: "Create New" }, { name: "List all" }]
    },
    {
        name: 'Market',
        iconClassName: "bi bi-basket-fill",
        to: '/Organiser',
        subMenu: [{ name: "Create New" }]
    },
]

const TestPage: NextPage = () => {
    const router = useRouter()
    const [category, setCategory] = useState("")
    const [item, setItem] = useState("");

    const handleSubjectSelect = (event) => {
        if (category === event.currentTarget.id) {
            setCategory("")
            setItem("")
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
        <div style={{ display: 'grid', gridTemplateColumns: '300px auto', height: "100vh" }}>
            <div>
                <Sidebar
                    menuItems={menuItems}
                    onItemSelect={handleItemSelect}
                    onSubjectSelect={handleSubjectSelect} />
            </div>
            <div style={{display: "flex", alignContent: "center", justifyContent:"center", height: "inherit"}}>
                {
                    (category === "Organisers" && item === "Create New") && <AddOrganiser />
                }
                {
                    (category === "Organisers" && item === "List all") && <OrganiserList />
                }
                {
                   (category === "Market" && item === "Create New") && <MarketForm />
                }
            </div>

        </div>
    )
}

export default TestPage;