using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Recipe.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    [Required(ErrorMessage = "The Recipe's name can't be empty!")]
    public string RecipeName { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public string Ingredients { get; set; }
    public List<RecipeTag> JoinEntities { get; set; }
    public ApplicationUser User { get; set; }
    public int Rating { get; set; }
    public static int CompareRecipeByRating(Recipe x, Recipe y)
    {
      if(x.Rating == y.Rating)
      {
        return 0;
      }
      if(x.Rating > y.Rating)
      {
        return 1;
      }
      if(x.Rating < y.Rating)
      {
        return -1;
      }
      return 0;
    }
  }
}