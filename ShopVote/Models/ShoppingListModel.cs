﻿using System;
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
        public DbSet<ShoppingListProducts> ShoppingListProducts { get; set; }
    }

    [Table("ShoppingList")]
    public class ShoppingList
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ShoppingListId { get; set; }
        
        public int UserId { get; set; }
        public string ListName { get; set; }

    }
  

    [Table("ShoppingListProducts")]
    public class ShoppingListProducts
    {
        [Column(Order=0),Key]
        public int ShoppingListId { get; set; }
        [Column(Order = 1), Key]
        public int ProductId { get; set; }
    }



}