using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages.Categories;

public class Update : PageModel
{
    public Category Category { get; set; } = null!;
    private CategoryRepository _categoryRepository;
    
    public Update(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void OnGet(int categoryId)
    {
        Category = _categoryRepository.Get(categoryId);
    }

    public IActionResult OnPost(Category category)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _categoryRepository.Update(category);

        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}