using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text;
using WebApplication3.Pages;
//DBCC CHECKIDENT('Product', RESEED, 0)
namespace WebApplication3.Models
{
    public class Product
    {
        
        
        
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole 'Nazwa' jest obowiązkowe!")]
        public string? name { get; set; }
        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Pole 'Cena' jest obowiązkowe!")]
        [Range(1, 999999, ErrorMessage = "Niepoprawna wartość ujemna")]
        public decimal? price { get; set; }
        [Display(Name = "Kategoria")]
        public string? category { get; set; }

        public static List<Product> GetProducts()
        {
            List<Product> data = new List<Product>();
            
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyCompany;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand command = new SqlCommand("sp_productView", con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product p = new Product();
                p.id=(int)reader["Id"];
                p.name=(string)reader["name"];
                p.price = (decimal)reader["price"];
                p.category = reader["category"] as  string;
                data.Add(p);
            }
            con.Close();
            return data;
        }
        
    }
}