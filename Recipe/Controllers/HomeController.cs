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
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(UserManager<IdentityUser> userManager, RecipeContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [HttpGet("/")]
    public async Task<ActionResult> Index()
    {
      string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      IdentityUser currentUser = await _userManager.FindByIdAsync(userId);
      Dictionary<string, object[]> model = new Dictionary<string, object[]>();
      if (currentUser != null)
      {
        Recipe.Models.Recipe[] recipes = _db.Recipes
                      .Where(entry => entry.User.Id == currentUser.Id)
                      .ToArray();

        model.Add("recipes", recipes);


        Tag[] tags = _db.Tags
                    .ToArray();
        model.Add("tags", tags);
      }
      return View(model);
    }
  }
}