import {useState, useRef, useEffect} from "react";
import {useNavigate} from "react-router-dom";

export interface iRecipe {
    name: string,
    description: string,
    cookingTime: iNumber,
    prepTime: iNumber,
    servings: iNumber,
    difficulty: iNumber,
    createdBy: iNumber,
    createdAt: iNumber,
    updatedAt: iNumber,
    imageUrl: string,
    id: iNumber
}

export interface iIngredient {
    name: string,
    description: string,
}

export type iNumber = number | null;

export default function AddRecipe({authToken}: {authToken: string}) {
    const [ingredients, setIngredients] = useState<number[]>([0]);
    const [instructions, setInstructions] = useState<number[]>([0]);
    const recipeFormRef = useRef<HTMLFormElement>(null);
    
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) navigate("/login", {state: {destination: "/recipes/add"}});
    }, []);
    
    function addIngredient() {
        setIngredients([...ingredients, ingredients.length]);
    }

    function addInstruction() {
        setInstructions([...instructions, instructions.length]);
    }
    
    async function submitRecipe() {
        if (recipeFormRef.current) {
            const formData = new FormData(recipeFormRef.current);
            const response = await fetch("Api/AddRecipe", {
                method: "POST",
                headers: {
                    "Authorization": "Bearer " + authToken,
                },
                body: formData,
            });
            console.log(response);
        }
    }

    return (<>
            <form ref={recipeFormRef}>
                <div>
                    <label htmlFor="Name">Name: </label>
                    <input type="text" name="Name" id="Name"/>
                </div>
                <div>
                    <label htmlFor="Description">Description: </label>
                    <textarea name="Description" id="Description" rows={5} cols={60}></textarea>
                </div>
                <div>
                    <label htmlFor="PrepTime">Prep time: </label>
                    <input type="number" name="PrepTime" id="PrepTime"/>
                </div>
                <div>
                    <label htmlFor="CookingTime">Cook time: </label>
                    <input type="number" name="CookingTime" id="CookingTime"/>
                </div>
                <div>
                    <label htmlFor="Servings">Servings: </label>
                    <input type="number" name="Servings" id="Servings"/>
                </div>
                <div>
                    <label htmlFor="Difficulty">Difficulty: </label>
                    <input type="number" name="Difficulty" id="Difficulty"/>
                </div>
                <div>
                    <label htmlFor="ImageUrl">Image Url: </label>
                    <input hidden value="ImplementLater" onChange={() => {}} name="ImageUrl" id="ImageUrl"/>
                </div>
                <div id="ingredientsList">
                    {ingredients.map((ingredient) => <div key={`ingredient-${ingredient}`}>
                        <label htmlFor={`Ingredients[${ingredient}].Name`}>Name: </label>
                        <input type="text" name={`Ingredients[${ingredient}].Name`}
                               id={`Ingredients[${ingredient}].Name`}/>
                    </div>)
                    }
                    <button type="button" onClick={addIngredient}>Add Ingredient</button>
                </div>
                <div id="instructionsList">
                    {instructions.map((instruction) => <div key={`instruction-${instruction}`}>
                        <label htmlFor={`Instructions[${instruction}].Name`}>Name: </label>
                        <input type="text" name={`Instructions[${instruction}].Description`}
                               id={`Instructions[${instruction}].Name`}/>
                        <input hidden type="number" name={`Instructions[${instruction}].Step`} value={instruction} onChange={() => {}}/>
                        
                    </div>)
                    }
                    <button type="button" onClick={addInstruction}>Add Instruction</button>
                </div>
            </form>
            <button type="button" onClick={submitRecipe}>Add Recipe</button>
        </>
    )
}