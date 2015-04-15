using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopVote.Models
{
    public class ShoppingListContext : DbContext
    {
        public ShoppingListContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<ShoppingList> ShoppingList { get; set; }
    }
    [Table("ShoppingList")]
    public class ShoppingList
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ShoppingListId { get; set; }

        //[ForeignKey("UserProfile")]
        public int UserId { get; set; }

        public string ListName { get; set; }
        public List<Product> shoppingList { get; set; }

    }
    public class CreateModel
    {
        [Required]
        [Display(Name = "List Name")]
        public string ListName { get; set; }
    }

}