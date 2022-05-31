using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3.Pages
{
    public class CategoryAddModel : PageModel
    {
        [BindProperty]
        public Category newCategory { get; set; }

        
        public void OnGet()
        {
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

                //string sql = "Insert into [Product](name,price) values (@name,@price)";
                SqlCommand cmd = new SqlCommand("sp_CategoryAdd", con);
                //SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                //int numProducts = (int)cmd1.ExecuteScalar();
                //cmd.Parameters.AddWithValue("@Id", numProducts++);
                cmd.Parameters.AddWithValue("@shortName", newCategory.shortName);
                cmd.Parameters.AddWithValue("@longName", newCategory.longName);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }

            return RedirectToPage("CategoryList");
        }
    }
}
