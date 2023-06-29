using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages.Categories;

public class Update : PageModel
{
    public Category Category { get; set; } = null!;
    
    public void OnGet(int categoryId)
    {
        Category = new CategoryRepository().Get(categoryId);
    }

    public IActionResult OnPost(Category category)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updatedCategory = new CategoryRepository().Update(category);

        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}