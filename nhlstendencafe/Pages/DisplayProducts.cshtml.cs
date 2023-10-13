using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages;

public class DisplayProducts : PageModel
{
    public IEnumerable<Product> ProductWithCategory { get; set; } = null!;
    [BindProperty(SupportsGet = true)]
    public Guid id { get; set; }
        
    
    public void OnGet([FromRoute]Guid SessionId)
    {
        ProductWithCategory = new ProductRepository().GetProductWithCategory();
        id = SessionId;
    }
}