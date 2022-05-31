using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace WebApplication3.Pages
{
    public class CategoryEditModel : PageModel
    {
        public List<Category> categoryList;
        [BindProperty]
        public Category EditCategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        public void OnGet()
        {
            categoryList = new List<Category>();
            categoryList = Category.GetCategorys();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyCompany;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                //string sql1 = " SELECT COUNT(*) FROM Product ";

                //string sql = "UPDATE [Product] SET name=@name, price=@price where Id=@ID";
                SqlCommand cmd = new SqlCommand("sp_categoryEdit", con);
                //SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                //int numProducts = (int)cmd1.ExecuteScalar();
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@shortName", EditCategory.shortName);
                cmd.Parameters.AddWithValue("@longName", EditCategory.longName);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                return RedirectToPage("CategoryList");
            }
        }
    }
}

