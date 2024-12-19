import './App.css'
import Login from "./pages/Login/Login.tsx";
import AddRecipe from "./pages/Recipes/AddRecipe.tsx";
import {
    createRoutesFromElements,
    createBrowserRouter,
    Route,
    RouterProvider
} from "react-router-dom";
import Layout from "./pages/Layout/Layout.tsx";
import Register from "./pages/Login/Register.tsx";
import RecipeList from "./pages/Recipes/RecipeList.tsx";
import RecipeDetail from "./pages/Recipes/RecipeDetail.tsx";

function App() {

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
                    element={<Login />}
                ></Route>
                <Route
                    path="recipe/:recipeId"
                    element={<RecipeDetail />}>
                </Route>
                <Route
                    path="recipes"
                    element={<RecipeList />}
                ></Route>
                <Route
                    path="recipes/add"
                    element={<AddRecipe />}
                ></Route>
                <Route
                    path="signup"
                    element={<Register/>}
                ></Route>
            </Route>))

    return (<>
            <RouterProvider router={router} fallbackElement={<p>Initial Load...</p>}/>
        </>
    )
}

export default App
