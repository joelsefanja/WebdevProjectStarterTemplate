using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;
using nhlstendencafe.SessionExtensions;
using Microsoft.AspNetCore.Http;

namespace nhlstendencafe.Pages.Products
{
    public class Index : PageModel
    {
        public IEnumerable<Product> ProductWithCategory { get; set; } = null!;
        public IEnumerable<Category> Categories {  get; set; } = null!;
        [BindProperty(SupportsGet = true)]
        public int CategoryId { get; set; }

        public void OnGet()
        {
            CategoryId = HttpContext.Session.GetObject<int>("CategoryId");
            ProductWithCategory = new ProductRepository().GetProductWithCategory();
            Categories = new CategoryRepository().Get();
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetObject<int>("CategoryId", CategoryId);
            return RedirectToPage(nameof(Update));
        }

        public IActionResult OnPostNewCategory()
        {
            HttpContext.Session.SetObject<int>("CategoryId", CategoryId);
            return RedirectToPage("/Categories/Create");
        }
    }
}
