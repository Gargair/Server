using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class News
    {
        [Key]
        public Guid NewsId { get; set; }
        [ScaffoldColumn(false)]
        public string Owner_Id { get; set; }
        [ForeignKey("Owner_Id")]
        public ApplicationUser Owner { get; set; }
        public string Text { get; set; }
        [Timestamp]
        public Byte[] TimeStamp { get; set; }

        public News()
        {
            this.NewsId = Guid.NewGuid();
        }

        public News(ApplicationUser Owner) : this()
        {
            this.Owner = Owner;
        }
    }
}