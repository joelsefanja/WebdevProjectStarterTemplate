using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages.Products;

public class Delete : PageModel
{
    public Product? Product { get; set; } = null!;
    public int ProductCount { get; set; }
    
    public void OnGet([FromRoute] int productId, [FromRoute] int productCount)
    {
        this.ProductCount = productCount;
        Product = new ProductRepository().GetProductById(productId);
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