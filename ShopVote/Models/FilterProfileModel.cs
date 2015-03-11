using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopVote.Models
{
    public class FilterProfilesContext : DbContext
    {
        public class FilterProfileModel
            : base ("DefaultConnection")
        {
        }
            public DbSet<FilterProfileModel> FilterProfiles{get;set;}
    }
    [Table("FilterProfiles")]
        public class FilterProfiles
    {
        [key]
        public int Id{get;set;}
        [ForeignKey("FilterCategory")]
        public int CategoryId {get;set;}
        [ForeignKey("Filter")]
        public int FilterId {get;set;}
        [ForeignKey("User")]
        public int UseryId {get;set;}
        public Object Value {get;set;}
    }
}
