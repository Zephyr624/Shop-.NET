using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Models;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication3.Pages
{
    
    public class CreateModel : MyPageModel
    {
        public SelectList Categorys { get; set; }
        [BindProperty]
        public Product newProduct { get; set; }
        public string category;
        private readonly ILogger<IndexModel> _logger;

        public IConfiguration _configuration { get; }
        
        
            public CreateModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {
            this.Categorys = new SelectList(Populate(),"id","longName");
        }
        private static List<Category> Populate()
        {
            string constr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyCompany;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            List<Category> Categorys = new List<Category>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                //string query = "SELECT FruitName, FruitId FROM Fruits";
                using (SqlCommand cmd = new SqlCommand("sp_categoryView", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Categorys.Add(new Category
                            {
                                longName = sdr["longName"].ToString(),
                                id = Convert.ToInt32(sdr["Id"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return Categorys;
        }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                string myCompanyDBcs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyCompany;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection con = new SqlConnection(myCompanyDBcs);
                SqlConnection con1 = new SqlConnection(myCompanyDBcs);
                //string sql1 = " SELECT COUNT(*) FROM Product ";

                //string sql = "Insert into [Product](name,price) values (@name,@price)";
                SqlCommand cmd = new SqlCommand("sp_productAdd", con);
                SqlCommand cmd1 = new SqlCommand("sp_categoryViewname", con1);
                //SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd1.CommandType = CommandType.StoredProcedure;
                con1.Open();
                cmd1.Parameters.AddWithValue("@ID", newProduct.category);
                SqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    category= (string)reader["longName"];
                }
                con1.Close();
                
                con.Open();
                //int numProducts = (int)cmd1.ExecuteScalar();
                //cmd.Parameters.AddWithValue("@Id", numProducts++);
                cmd.Parameters.AddWithValue("@name", newProduct.name);
                cmd.Parameters.AddWithValue("@price", newProduct.price);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd1.Dispose();
		        con.Close();
            }

            return RedirectToPage("List");
         }
            
        
    }
}