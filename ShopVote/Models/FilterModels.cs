﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopVote.Models
{
  public class FiltersContext : DbContext 
  {
    public FiltersContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<Filter> Filters { get; set; }
  }

  [Table("Filters")]
  public class Filter
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("FilterCategory")]
    public int CategoryId { get; set; }

    [ForeignKey("Manufacturer")]
    public int ManufacturerId { get; set; }

    // TODO: Add Foreign Key to product after model has been created

    public string Name { get; set; }
    public Object Value { get; set; }
  }

  public class FilterCategoriesContext : DbContext
  {
    public FilterCategoriesContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<Filter> FilterCategories { get; set; }
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