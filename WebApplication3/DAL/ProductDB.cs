using System;
using System.Collections.Generic;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication3.Models;
namespace WebApplication3.DAL
{
    public class ProductDB
    {
        private List<Product>? products;
        public void Load(string jsonProducts)
        {
            if (jsonProducts == null)
            {
                //products.Add(new Product());
            }
            else
            {
                // products = JsonConvert.DeserializeObject<List<Product>>(jsonProducts);
                products = JsonSerializer.Deserialize<List<Product>>(jsonProducts);
            }
        }

        private int GetNextId()
        {
            int lastID = products[products.Count -1].id;
            int newID = lastID;
            newID++;
            return newID;
        }
        public void Delete(Product p)
        {
            
            products.Remove(p);
        }

        public void Create(Product p)
        {
            p.id = GetNextId();
            products.Add(p);
        }
        public void Edit(Product p,int id,string name,decimal? price)
        {
            products[id].id=id+1;
            products[id].name=name;
            products[id].price=price;
        }
        public string Save()
        {

            //return JsonConvert.SerializeObject(products);
            return JsonSerializer.Serialize(products);
        }
        public List<Product> List()
        {
            return products;
        }
    }
}