using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopVote.Models
{
  public class ProductFiltersContext : DbContext 
  {
      public ProductFiltersContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<ProductFilter> Filters { get; set; }

    public DbSet<FilterCategory> FilterCategories { get; set; }

    public DbSet<Manufacturer> Manufacturers { get; set; }

    public DbSet<Product> Products { get; set; }
  }

  [Table("ProductFilters")]
  public class ProductFilter
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("FilterCategory")]
    public int CategoryId { get; set; }

    public virtual FilterCategory FilterCategory { get; set; }

    [ForeignKey("Manufacturer")]
    public int ManufacturerId { get; set; }

    public virtual Manufacturer Manufacturer { get; set; }

    [ForeignKey("Product")]
    public int? ProductId { get; set; }

    public virtual Product Product { get; set; }

    public string Name { get; set; }
    public Object Value { get; set; }
  }

  public class FilterCategoriesContext : DbContext
  {
    public FilterCategoriesContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<ProductFilter> FilterCategories { get; set; }
  }

  [Table("FilterCategories")]
  public class FilterCategory
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
  }

}