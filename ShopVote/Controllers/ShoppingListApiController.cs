using ShopVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopVote.Models;
namespace ShopVote.Controllers
{
    public class ShoppingListApiController : ApiController
    {
        private ShoppingListContext db = new ShoppingListContext();
        private UsersContext du = new UsersContext();

        public string GetShoppingListById(int id)
        {
            string result = "";
            var list = db.ShoppingList.Find(id);

            return result+list.ListName;
        }

        public string GetShoppingList(int userId)
        {
            string result = "";
            var list = db.ShoppingList.Where(p => p.UserId == userId);
            foreach (var thing in list)
            {
                result = result + thing.ShoppingListId + ",";
            }

            return result;
        }
        
        public string GetShoppingListItems(int listId)
        {
            string result = "";
            var list = db.ShoppingListProducts.Where(p => p.ShoppingListId == listId);
            foreach (var thing in list)
            {
                result = result + thing.ProductId + "," ;
            }

            return result;
        }

        public int GetUserByUserName(string name)
        {
            var list = du.UserProfiles.Where(p => p.UserName == name);
            foreach (var thing in list)
            {
                return thing.UserId;
            }
            return -1;
        }

        public HttpResponseMessage PostProductToList(int productId, int listId)
        {
            ShoppingListProducts element = new ShoppingListProducts();
            element.ShoppingListId = listId;
            element.ProductId = productId;
            db.ShoppingListProducts.Add(element);
            db.SaveChanges();

            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Content = new StringContent("You reached here");
            return response;
        }
        /*
        public HttpResponseMessage DeleteProduct(int deleteProductId)
        {
            // Delete the movie from the database
            ShoppingList list = db.ShoppingList.Find(deleteProductId);
            var elements = (from y in db.ShoppingListProducts where y.ShoppingListId == deleteProductId select y);
            foreach (var item in elements)
            {
                db.ShoppingListProducts.Remove(item);
            }

            db.ShoppingList.Remove(list);
            db.SaveChanges();
            // Return status code
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
        */
    }
}
