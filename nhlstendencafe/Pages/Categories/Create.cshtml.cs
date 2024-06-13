using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Pages;
using nhlstendencafe.Repositories;
using Org.BouncyCastle.Ocsp;
using Index=nhlstendencafe.Pages.Account.Index;

namespace nlstendencafe.Pages.Categories;


public class Create : PageModel
{
    [BindProperty] 
    public Category Category { get; set; } = null!;
    
    [BindProperty]
    public string returnUrl { get; set; }

    public void OnGet()
    {
        returnUrl = Request.Headers["Referer"].ToString();
    }

    public IActionResult OnPost()
    {

        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        new CategoryRepository().Add(Category);
        return Redirect(returnUrl);
    }
    public IActionResult OnPostCancel()
    {
        return Redirect(returnUrl);
    }
}