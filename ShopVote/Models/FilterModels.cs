using System;
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

    public DbSet<PMFilter> Filters { get; set; }
  }

  [Table("Filters")]
  public class PMFilter
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public virtual int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public virtual FilterCategory Category { get; set; }

    public virtual int QuestionID { get; set; }
    [ForeignKey("QuestionID")]
    public virtual Question Question { get; set; }

    public virtual int ManufacturerID { get; set; }
    [ForeignKey("ManufacturerID")]
    public virtual Manufacturer Manufacturer { get; set; }

    public virtual int? ProductID { get; set; }
    [ForeignKey("ProductID")]
    public virtual Product Product { get; set; }

    [Display(Name = "Value")]
    public decimal FilterValue { get; set; }
  }

  public class FilterCategoriesContext : DbContext
  {
    public FilterCategoriesContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<FilterCategory> FilterCategories { get; set; }
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