import {useState} from 'react'
import './App.css'
import Login from "./pages/Login/Login.tsx";
import AddRecipe, {iRecipe} from "./pages/AddRecipe/AddRecipe.tsx";
import {
    createRoutesFromElements,
    createBrowserRouter,
    Route,
    RouterProvider
} from "react-router-dom";
import Layout from "./pages/Layout/Layout.tsx";
import Register from "./pages/Login/Register.tsx";

function Recipe({recipe}: { recipe: iRecipe }) {
    return (<div className="recipe-container">
        <h3 className="recipe-name">{recipe.name}</h3>
        <div className="recipe-description">{recipe.description}</div>
        <div className="recipe-preptime">Prep time: {recipe.prepTime}</div>
        <div className="recipe-cookingtime">Cook time: {recipe.cookingTime}</div>
        <div className="recipe-servings">Servings: {recipe.servings}</div>
        <div className="recipe-difficulty">Difficulty: {recipe.difficulty}</div>
    </div>)
}


function App() {
    const [recipes, setRecipes] = useState<iRecipe[]>([]);
    const [authToken, setAuthToken] = useState("");

    const router = createBrowserRouter(
        createRoutesFromElements(
            <Route
                path="/"
                element={<Layout/>}
                // loader={rootLoader}
                // action={rootAction}
                // errorElement={<ErrorPage />}
            >
                <Route
                    path="signin"
                    element={<Login setAuthToken={setAuthToken}/>}
                ></Route>
                <Route
                    path="recipes"
                    element={<Recipes/>}
                ></Route>
                <Route
                    path="recipes/add"
                    element={<AddRecipe authToken={authToken}/>}
                ></Route>
                <Route
                    path="signup"
                    element={<Register/>}
                ></Route>
            </Route>))


    async function getRecipes() {
        if (recipes.length > 0) return recipes;
        const response = await fetch('Api/GetRecipeList');
        const apiRecipes = await response.json();
        setRecipes(apiRecipes);
    }

    function Recipes() {
        getRecipes();
        return (<div>
            {recipes.length > 0 && recipes.map(recipe => <Recipe recipe={recipe} key={recipe.id}/>)}
        </div>)
    }

    return (<>
            <RouterProvider router={router} fallbackElement={<p>Initial Load...</p>}/>
        </>
    )
}

export default App
