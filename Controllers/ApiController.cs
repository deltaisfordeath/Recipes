using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipes.Models;
using Recipes.Services;

namespace Recipes.Controllers;

public class ApiController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RecipeService _recipeService;
    private readonly UserManager<RecipeUser> _userManager;

    private const string Name = "api";

    public ApiController(
        ILogger<HomeController> logger, 
        RecipeService recipeService, 
        UserManager<RecipeUser> userManager)
    {
        _logger = logger;
        _recipeService = recipeService;
        _userManager = userManager;
    }

    [Route($"{Name}/Recipe/List")]
    public async Task<IActionResult> GetRecipePage(int pageNum)
    {
        var recipes = await _recipeService.GetPageOfRecipes(pageNum);
        var response = new PaginatedRecipeViewModel
        {
            items = recipes.Items,
            hasNextPage = recipes.HasNextPage
        };
        return new JsonResult(response);
    }

    [HttpGet]
    [Route($"{Name}/Recipe/{{recipeId:int}}")]
    public async Task<IActionResult> GetRecipe(int recipeId)
    {
        var recipe = await _recipeService.GetFullRecipe(recipeId);
        if (recipe == null)
        {
            return StatusCode(404);
        }

        var recipeResult = new RecipeViewModel(recipe);

        return new JsonResult(recipeResult);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRecipeList()
    {
        var recipes = _recipeService.GetAllRecipes();
        return new JsonResult(recipes);
    }
    
    [HttpPost, Authorize]
    [Route($"{Name}/Recipe/Add")]
    public async Task<IActionResult> AddRecipe(RecipeViewModel viewModel)
    {
        var userId = _userManager.GetUserId(this.User);
        
        if (viewModel.Ingredients == null || viewModel.Ingredients.Count < 1
                                          || viewModel.Instructions == null || viewModel.Instructions.Count < 1)
        {
            return new JsonResult("Please provide a complete recipe with ingredients and instructions.");
        }

        var createdAt = DateTime.Now;

        var recipe = new Recipe
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            CookingTime = viewModel.CookingTime,
            PrepTime = viewModel.PrepTime,
            Servings = viewModel.Servings,
            Difficulty = viewModel.Difficulty,
            ImageUrl = viewModel.ImageUrl,
            CreatedBy = userId,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };

        await _recipeService.AddRecipe(recipe, viewModel.Ingredients, viewModel.Instructions, recipe.CreatedBy);

        return RedirectToAction(nameof(Index), Name);
    }

    [HttpGet]
    public async Task<IActionResult> CheckEmail(string email)
    {
        var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

        return existingUser != null ? new JsonResult(true) : new JsonResult(false);
    }
}