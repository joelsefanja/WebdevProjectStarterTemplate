using System.Collections;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages;

public class DisplayCategories : PageModel
{
    public IEnumerable<Category> Categories { get; set; } = null!;
    
    public void OnGet()
    {
        Categories = new CategoryRepository().GetCategoriesWithProducts();
    }

    
}