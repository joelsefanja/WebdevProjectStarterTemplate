using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages.Products;

public class Update : PageModel
{
    public Product Product { get; set; } = null!;
    
    public void OnGet(int productId)
    {
        Product = new ProductRepository().GetProductById(productId);
    }

    public IActionResult OnPost()
    {
        // if (!ModelState.IsValid)
        // {
        //     return Page();
        // }
        
        if (Product == null)
        {
            return NotFound();
        }

        var repository = new ProductRepository();
        repository.UpdateProduct(Product);

        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}