using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages.Products;

public class Index : PageModel
{
    ProductRepository productRepository = new ProductRepository();
    public IEnumerable<Product> products = null;
    
    public void OnGet()
    {
        products = productRepository.GetProductWithCategory();
    }
}