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
        public DbSet<Question> Questions { get; set; }
  }

  [Table("FilterProfiles")]
  public class FilterProfile {

    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public virtual int UserID { get; set; }
    [ForeignKey("UserID")]
    public virtual UserProfile User { get; set; }

    public virtual int QuestionID { get; set; }
    [ForeignKey("QuestionID")]
    public virtual Question Question { get; set; }

    public virtual int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public virtual FilterCategory Category { get; set; }

    public decimal Value { get; set; }

  }

  public class FPValues
  {
    public string[] Categories { get; set; }
    public string[] Questions { get; set; }
    public string[] Values { get; set; }
  }

  [Table("Questions")]
  public class Question {

    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public virtual int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public virtual FilterCategory Category { get; set; }

    public string Description { get; set; }

  }

}