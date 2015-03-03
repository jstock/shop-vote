using System;
using System.collections.generic;
using System.componentModel.DataAnnotations;
using System.componentModel.DataAnnonnotaions.schema;
using System.data.Entity;
using System.Linq;
using System.web;

namespace ShopVote.Models
{
     public class FilterProfilesContext : DbContext
     {
        public FilterProfilesContext()
         :  base("DefaultConnection")
        {
        }
        public DbSet<Filter> FilterProfiles { get;set; }
    }

    [Table("FilterProfiles")]
    public class FilterProfile
    {
     [key]
     [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
     public int Id  {get;set;}
     [ForeigKey("User")]
     public int UserId{get;set;}
     [ForeigKey("Filter")]
     public int FilterId{get;set;}
     [ForeigKey("FilterCategory")]
     public int CategoryId{get;set;}
     public Object Value {get;set;}
    }
}