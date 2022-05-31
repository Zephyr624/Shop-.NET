using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text;
using WebApplication3.Pages;
namespace WebApplication3.Models
{
    public class Category
    {
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name = "Krótka Nazwa")]
        [Required(ErrorMessage = "Pole 'Nazwa' jest obowiązkowe!")]
        public string? shortName { get; set; }
        [Display(Name = "Długa Nazwa")]
        [Required(ErrorMessage = "Pole 'Nazwa' jest obowiązkowe!")]
        public string? longName { get; set; }
        public static List<Category> GetCategorys()
        {
            List<Category> data = new List<Category>();

            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyCompany;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand command = new SqlCommand("sp_categoryView", con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Category p = new Category();
                p.id = (int)reader["Id"];
                p.shortName = (string)reader["shortName"];
                p.longName = (string)reader["longName"];
                data.Add(p);
            }
            con.Close();
            return data;
        }
    }
}
