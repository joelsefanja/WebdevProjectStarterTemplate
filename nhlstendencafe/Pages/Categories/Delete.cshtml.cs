using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages.Categories;

public class Delete : PageModel
{
    private readonly CategoryRepository _categoryRepository;

    public Delete(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [BindProperty (SupportsGet = true)]
    public string? Category { get; set; }

    public bool CategoryHasProducts { get; set; }

    public void OnGet(int categoryId, string category)
    {
        CategoryHasProducts = _categoryRepository.CategoryHasProducts(categoryId);
        Category = category;
    }

    public IActionResult OnPostDeleteAsync(int categoryId)
    {
        _categoryRepository.Delete(categoryId);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}
