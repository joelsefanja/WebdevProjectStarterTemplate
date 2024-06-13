using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;
using nhlstendencafe.SessionExtensions;


namespace nhlstendencafe.Pages.Products {
    public class Create : PageModel {
        [BindProperty] public Product Product { get; set; } = new Product(); 
        public IEnumerable<Category> Categories { get; set; } = null!;
        [BindProperty (SupportsGet = true)] int CategoryId { get; set; }

        public void OnGet()
        {
            Categories = new CategoryRepository().Get();
        }

        public IActionResult OnPost()
        {
            Categories = new CategoryRepository().Get();
            HttpContext.Session.SetObject<int>("CategoryId", CategoryId);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            new ProductRepository().AddProduct(Product);
            return RedirectToPage(nameof(Index), new { categoryId = Product.CategoryId });
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index), new { categoryId = Product.CategoryId });
        }
    }
}
