using System.Collections.Generic;

namespace Recipe.Models
{
  public class Tag
    {
        public int TagId { get; set; }
        public string Title { get; set; }
        public List<RecipeTag> JoinEntities { get;}
        public ApplicationUser User { get; set; }
    }
}