using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;

namespace WebApplication3.Pages
{
    public class CategoryDetailsModel : PageModel
    {
        public List<Category> categoryList;
        public Category Category { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        public void OnGet()
        {
            categoryList = new List<Category>();
            categoryList = Category.GetCategorys();
        }

        public IActionResult OnPost()
        {

            return RedirectToPage("CategoryList");
        }
    }
}
