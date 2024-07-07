using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;
using Recipes.Services;

namespace Recipes.Controllers;

public class RecipesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RecipeService _recipeService;

    public static readonly string Name = "Recipes";

    public RecipesController(ILogger<HomeController> logger, RecipeService recipeService)
    {
        _logger = logger;
        _recipeService = recipeService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var recipes = _recipeService.GetAllRecipes();
        var viewModel = new RecipeIndexViewModel
        {
            Recipes = recipes
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetRecipeList()
    {
        var recipes = _recipeService.GetAllRecipes();
        return new JsonResult(recipes);
    }

    [HttpGet]
    public async Task<IActionResult> AddRecipe()
    {
        var viewModel = new RecipeViewModel();

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddRecipe(RecipeViewModel viewModel)
    {
        
        if (viewModel.Ingredients == null || viewModel.Ingredients.Count < 1
            || viewModel.Instructions == null || viewModel.Instructions.Count < 1)
        {
            return View(viewModel);
        }

        var recipe = new Recipe
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            CookingTime = viewModel.CookingTime,
            PrepTime = viewModel.PrepTime,
            Servings = viewModel.Servings,
            Difficulty = viewModel.Difficulty,
            ImageUrl = viewModel.ImageUrl
        };

        await _recipeService.AddRecipe(recipe, viewModel.Ingredients, viewModel.Instructions, "Donkey");

        return RedirectToAction(nameof(Index), Name);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int Id)
    {
        var recipe = await _recipeService.GetFullRecipe(Id);

        if (recipe == null)
        {
            return RedirectToAction(nameof(Index));
        }

        var viewModel = new RecipeViewModel(recipe);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> GetIngredientSuggestions([FromBody] string nameSubstring)
    {
        var ingredients = await _recipeService.GetIngredientSuggestions(nameSubstring);
        if (ingredients != null)
        {
            string[] apiIngredients = ingredients.Select(_ => _.Name).ToArray();
            return new JsonResult(JsonSerializer.Serialize(apiIngredients));
        }

        return StatusCode(404);
    }

    public async Task<IActionResult> AddIngredient()
    {
        var viewModel = new IngredientViewModel();

        return View(viewModel);
    }
}