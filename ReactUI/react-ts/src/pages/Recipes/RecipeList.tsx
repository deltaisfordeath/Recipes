import {useEffect, useState} from "react";
import {iRecipe} from "./AddRecipe.tsx";
import {Link} from "react-router-dom";

export function Recipe({recipe}: { recipe: iRecipe }) {
    return ( <Link to={`/recipe/${recipe.id}`}>
        <div className="recipe-container">
        <h3 className="recipe-name">{recipe.name}</h3>
        <div className="recipe-description">{recipe.description}</div>
        <div className="recipe-preptime">Prep time: {recipe.prepTime}</div>
        <div className="recipe-cookingtime">Cook time: {recipe.cookingTime}</div>
        <div className="recipe-servings">Servings: {recipe.servings}</div>
        <div className="recipe-difficulty">Difficulty: {recipe.difficulty}</div>
    </div>
    </Link>)
}

export default function RecipeList() {
    const [recipes, setRecipes] = useState<iRecipe[]>([]);
    const [hasNextPage, setHasNextPage] = useState(true);
    const [pageNum, setPageNum] = useState(1);

    async function getRecipes() {
        if (!hasNextPage) return; 
        const response = await fetch(`Api/Recipe/List?pageNum=${pageNum}`);
        setPageNum(pageNum + 1);
        const json = await response.json();
        const apiRecipes = json.items;
        setRecipes([...recipes, ...apiRecipes]);
        setHasNextPage(json.hasNextPage);
    }

    useEffect(() => {
        getRecipes();
    }, []);

    function Recipes() {
        return (<div>
            {recipes.length > 0 && recipes.map(recipe => <Recipe recipe={recipe} key={recipe.id}/>)}
            {hasNextPage && <button onClick={getRecipes}>Next</button>}
        </div>)
        
    }
    
    return(
        <Recipes />
    )
}