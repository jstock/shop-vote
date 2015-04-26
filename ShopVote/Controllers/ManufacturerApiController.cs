using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopVote.Models;

namespace ShopVote.Controllers
{
    public class ManufacturerApiController : ApiController
    {
        private ManufacturersContext db = new ManufacturersContext();
        public String getManufacturerByManId(int manId)
        {
            Manufacturer man = db.Manufacturers.Find(manId);
            return man.Name;
        }


    }
}
