using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopVote.Models;
using System.Web.Mvc;

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
    }
}
