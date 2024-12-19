import {iRecipe} from "./AddRecipe.tsx";
import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";

export default function RecipeDetail() {
    const [recipe, setRecipe] = useState<iRecipe | null>(null);
    const params = useParams()

    useEffect(() => {
        if (params.recipeId) {
            getRecipe();
        }
    }, []);
    
    async function getRecipe() {
        const recipe = await fetch(`/Api/Recipe/${params.recipeId}`);
        setRecipe(await recipe.json() as unknown as iRecipe);
    }
    
    return <>
        <h1>{recipe?.name}</h1>
        <p>{recipe?.description}</p>
        <h3>Ingredients</h3>
        {recipe?.ingredients && recipe.ingredients.map(ingredient => <p key={ingredient.name}>{ingredient.name}</p>)}
        <h3>Instructions</h3>
        {recipe?.instructions && recipe.instructions.map(instruction => <p key={instruction.step}>{instruction.step}. {instruction.description}</p>)}
        
        </>
}