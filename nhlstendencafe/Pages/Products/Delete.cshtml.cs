using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages.Products;

public class Delete : PageModel
{
    public Product product { get; set; } = null!;
    
    public void OnGet([FromRoute] int productId)
    {
        product = new ProductRepository().GetProductById(productId);
    }

    public IActionResult OnPostDelete([FromRoute]int productId)
    {
        new ProductRepository().DeleteProduct(productId);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}