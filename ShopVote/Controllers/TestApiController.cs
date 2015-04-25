using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopVote.Models;

namespace ShopVote.Controllers
{
    public class TestApiController : ApiController
    {
        public Product getProduct(int id)
        {

            // Return movie by id
            if (id == 1)
            {
                return new Product
                {
                    Id = 1,
                    Name = "Test Product",
                    Description = "this is a test"
                };
            }

            // Otherwise, movie was not found
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
