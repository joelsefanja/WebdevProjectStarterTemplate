using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages.Products;

public class Update : PageModel 
{
    private  ProductRepository _productRepository;

    public IEnumerable<Category> Categories { get; set; } = null!;
    
    [BindProperty] public Product Product { get; set; } = new Product(); 

    public Update()
    {
        _productRepository = new ProductRepository();
    }
    
    public void OnGet(int productId)
    {
        _productRepository = new ProductRepository();
        Product = _productRepository.GetProductById(productId) ?? throw new Exception("Product not found");
        Categories = new CategoryRepository().Get();
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        _productRepository.UpdateProduct(Product);

        return RedirectToPage(nameof(Index), new { categoryId = Product.CategoryId });
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index), new { categoryId = Product.CategoryId });
    }
}
