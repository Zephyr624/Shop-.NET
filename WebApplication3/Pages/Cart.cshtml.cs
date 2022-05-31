using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;

namespace WebApplication3.Pages
{
    
        public class CartModel : MyPageModel
        {
            public List<Product> productList;
            public void OnGet()
            {
                LoadDB();
                productList = productDB.List();
                SaveDB();
            }
        }
    

}
