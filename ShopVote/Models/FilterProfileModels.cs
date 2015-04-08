using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopVote.Models
{
  public class FilterProfilesContext : DbContext {

      public FilterProfilesContext() :
        base("DefaultConnection") {
        }
        public DbSet<FilterProfile> FilterProfiles { get; set; }
  }

  [Table("FilterProfiles")]
  public class FilterProfile {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id;

    public virtual int UserID { get; set; }
    [ForeignKey("UserID")]
    public virtual UserProfile User { get; set; }

    public virtual int FilterID { get; set; }
    [ForeignKey("FilterID")]
    public virtual PMFilter Filter { get; set; }

    public virtual int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public virtual FilterCategory Category { get; set; }

    public Object Value { get; set; }

  }

}