using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nlstendencafe.Pages.Categories;


public class Create : PageModel
{
    [BindProperty] public Category Category { get; set; } = null!;
    
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        new CategoryRepository().Add(Category);
        return RedirectToPage(nameof(System.Index));
    }

    public IActionResult OnPostCancel()
    {
        return Redirect(nameof(System.Index));
    }
}