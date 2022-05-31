using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;
using System.Data.SqlClient;
using System.Text;
using System.Data;
namespace WebApplication3.Pages
{
    public class DeleteModel : MyPageModel
    {
        
        public List<Product> productList;
        public Product Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        public void OnGet()
        {
            productList = new List<Product>();
            productList = Product.GetProducts();
        }
        public IActionResult OnPost()
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyCompany;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //string sql1 = " SELECT COUNT(*) FROM Product ";
            
            //string sql = "Delete from [Product] where Id=@ID";
            SqlCommand cmd = new SqlCommand("sp_productDelete", con);
            //SqlCommand cmd1 = new SqlCommand(sql1, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            //int numProducts = (int)cmd1.ExecuteScalar();
            cmd.Parameters.AddWithValue("@ID", ID);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return RedirectToPage("List");
        }
    }
}
