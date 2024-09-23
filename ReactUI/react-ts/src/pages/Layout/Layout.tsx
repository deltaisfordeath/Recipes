import Nav from "../Nav/Nav.tsx";
import {Outlet} from "react-router-dom";

export default function Layout() {
    return(<div className={"app-container"}>
        <Nav />
        <Outlet />
    </div>
        
    )
}