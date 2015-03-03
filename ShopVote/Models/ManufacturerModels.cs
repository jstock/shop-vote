using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopVote.Models
{
  public class ManufacturersContext : DbContext
  {
    public ManufacturersContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<Manufacturer> Manufacturers { get; set; }
  }

  [Table("Manufacturer")]
  public class Manufacturer
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
  }
}