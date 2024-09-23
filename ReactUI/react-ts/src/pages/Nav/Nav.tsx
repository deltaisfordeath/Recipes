import {Link} from "react-router-dom";

export default function Nav() {
    return (<div className={"navbar"}>
            <Link to={"/Recipes"}>Browse Recipes</Link>
            <Link to={"/Recipes/Add"}>Add a Recipe</Link>
            <Link to={"/signin"}>Log in</Link>
            <Link to={"/signup"}>Register</Link>
        </div>
    )
}