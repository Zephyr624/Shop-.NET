using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;

namespace WebApplication3.Pages
{
    public class DetailsModel : MyPageModel
    {
        public List<Product> productList;
        public Product Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        public void  OnGet()
        {
            productList = new List<Product>();
            productList = Product.GetProducts();
        }

        public IActionResult OnPost()
        {
            
            return RedirectToPage("List");
        }
        
    }
}
