using Microsoft.AspNetCore.Mvc;
using Recipe.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Recipe.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    private readonly RecipeContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager, RecipeContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    [HttpGet("/")]
    public ActionResult Index()
    {

      Dictionary<string, object[]> model = new Dictionary<string, object[]>();

        List<Recipe.Models.Recipe> recipes = _db.Recipes.ToList();

        recipes.Sort(Recipe.Models.Recipe.CompareRecipeByRating);

        Recipe.Models.Recipe[] recipesSorted = recipes.ToArray();

        model.Add("recipes", recipesSorted);


        Tag[] tags = _db.Tags
                    .ToArray();
        model.Add("tags", tags);

      return View(model);
    }
  }
}