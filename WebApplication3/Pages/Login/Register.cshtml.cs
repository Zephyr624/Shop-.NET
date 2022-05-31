using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;

namespace WebApplication3.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }
        /*private bool ValidateUser(SiteUser user)
        {
            if ((user.userName == "admin") && (user.password == "abc"))
                return true;
            else
                return false;
        }*/
        private void AddUser(SiteUser user)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyCompany;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand com = new SqlCommand("Register", con);
            com.CommandType = CommandType.StoredProcedure;
            //SqlParameter p1 = new SqlParameter("username", TextBoxusername.Text);
            //SqlParameter p2 = new SqlParameter("password", TextBoxpassword.Text);
            com.Parameters.AddWithValue("@username", user.userName);
            com.Parameters.AddWithValue("@password", user.password);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            AddUser(user);
            return RedirectToPage("/List");
        }
    }
}
