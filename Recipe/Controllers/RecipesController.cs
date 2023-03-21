using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Recipe.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Recipe.Controllers
{
  [Authorize]
  public class RecipeController : Controller
  {
    private readonly RecipeContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public RecipeController(UserManager<IdentityUser> userManager, RecipeContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      IdentityUser currentUser = await _userManager.FindByIdAsync(userId);
      
      List<Recipe.Models.Recipe> userItems = _db.Recipes
                          .ToList();
      userItems.Sort(Recipe.Models.Recipe.CompareRecipeByRating);
      return View(userItems);
    }

    public async Task<ActionResult> Create()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      IdentityUser currentUser = await _userManager.FindByIdAsync(userId);
      //ViewBag.Tags = _db.Tags.ToList();
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe.Models.Recipe recipe)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      IdentityUser currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = (ApplicationUser)currentUser;
      _db.Recipes.Add(recipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}