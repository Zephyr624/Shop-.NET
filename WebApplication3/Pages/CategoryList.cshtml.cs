using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
namespace WebApplication3.Pages
{
    public class CategoryListModel : MyPageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IConfiguration _configuration { get; }
        public CategoryListModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        //public string lblInfoText;
        public List<Category> categoryList;
        public void OnGet()
        {
            categoryList = new List<Category>();
            categoryList = Category.GetCategorys();
            /*string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Product";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            StringBuilder htmlStr = new StringBuilder("");
            while (reader.Read())
            {
                htmlStr.Append("<li>");
                htmlStr.Append(reader["Id"].ToString() + " ");
                htmlStr.Append(reader.GetString(1) + " ");
                htmlStr.Append(String.Format("{0:0.00}",
               Decimal.Parse(reader["Price"].ToString())));
                htmlStr.Append("</li>");
            }
            reader.Close(); con.Close();
            lblInfoText = htmlStr.ToString();*/

        }
    }
}