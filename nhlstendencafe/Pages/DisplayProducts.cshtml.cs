using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages;

public class DisplayProducts : PageModel
{
    public IEnumerable<Product> ProductWithCategory { get; set; } = null!;
    
    public void OnGet()
    {
        ProductWithCategory = new ProductRepository().GetProductWithCategory();
    }
}