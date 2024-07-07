import {Link, Outlet} from "react-router-dom";

export default function Nav() {
    return (<>
            <Link to={"/Recipes"}>Browse Recipes</Link>
            <Link to={"/Recipes/Add"}>Add a Recipe</Link>
            <Link to={"/login"}>Log in</Link>
            
            <Outlet />
        </>
    )
}