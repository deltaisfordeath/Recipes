using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;

namespace Recipes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RecipeRepository _recipeRepository;

    public HomeController(ILogger<HomeController> logger, RecipeRepository recipeRepository)
    {
        _logger = logger;
        _recipeRepository = recipeRepository;
    }

    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
