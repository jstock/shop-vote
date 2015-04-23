using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopVote.Models
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Feedback> Feedbacks { get; set; }
    }

    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Content { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

    }
}