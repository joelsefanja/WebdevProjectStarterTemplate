using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nlstendencafe.Pages.Products
{
    public class Create : PageModel
    {
        [BindProperty] public Product Product { get; set; } = null!;
        
        public IEnumerable<Category> Categories { get; set; } = new CategoryRepository().GetCategoryNames();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
         
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }
            
            var newProduct = new ProductRepository().AddProduct(Product);
            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancel()
        {
            return Redirect(nameof(Index));
        }
    }
}