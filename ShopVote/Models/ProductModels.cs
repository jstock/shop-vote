using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopVote.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
    }

    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Filter")]
        public int FilterId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

    }
}