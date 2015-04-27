using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopVote.Models;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Data;
using System.Data.Entity;

namespace ShopVote.Controllers
{
    public class ProductApiController : ApiController
    {
        private ProductContext db = new ProductContext();

        public string GetProduct(int id, string type)
        {
            // Return movie by id
            Product product = db.Products.Find(id);
            if (product == null)
            {
                // Otherwise, movie was not found
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            string result = "";
            switch (type)
            {
                case "name":
                    result = product.Name;
                    break;
                case "desc":
                    result = product.Description;
                    break;
                case "man":
                    result = product.Manufacturer.Name;
                    break;

            }

            return result;
        }


        public List<Product> GetAllProduct()
        {
            List<Product> entityTypes = new List<Product>();
            foreach (Product i in db.Products)
            {
                entityTypes.Add(i);
            }
            return entityTypes;
        }

        public int GetUserByNameAndPassword(string username, string password)
        {
            if (WebSecurity.Login(username, password))
            {
                return WebSecurity.GetUserId(username);
            }
            return -1;
        }

        public string GetProductByName(string searchText)
        {
            string result = "";
            var products = db.Products.Where(p => p.Id > 0).Include(p => p.Manufacturer);

            if (!String.IsNullOrWhiteSpace(searchText))
            {
                products = products.Where(p => p.Name.Contains(searchText) || p.Description.Contains(searchText));
            } 
            foreach (var thing in products)
            {
                result = result + thing.Id + ",";
            }

            return result;
        }
    }
}
