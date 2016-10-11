using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Server.Models
{
    public class Base
    {
        [Key]
        public Guid baseId {get; set;}
        [MinLength(5), MaxLength(20)]
        [DisplayName("Name")]
        public string name { get; set; }
        [ScaffoldColumn(false)]
        public string Owner_Id { get; set; }
        [ForeignKey("Owner_Id")]
        public ApplicationUser Owner { get; set; }
        [Timestamp]
        public Byte[] TimeStamp { get; set; }

        private Base()
        {
            baseId = Guid.NewGuid();
        }

        public Base(ApplicationUser owner) : this()
        {
            this.Owner = owner;
            this.name = "Neue Kolonie";
        }
    }
}