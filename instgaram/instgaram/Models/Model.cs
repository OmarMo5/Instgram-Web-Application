﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace instgaram.Models
{
    public class Model : DbContext
    {
        public Model() : base("Inst85")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<post> posts { get; set; }
        public DbSet<comment> comments { get; set; }
        public DbSet<friend_re> friend_Res { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Photo { get; set; }
        public virtual List<post> posts { get; set; }
        public virtual List<comment> Comments { get; set; }
    }
    public class post
    {
        public int Id { get; set; }
        public string photo { get; set; }
        public virtual User user { get; set; }
        public virtual List<comment> Comments { get; set; }
    }

    public class comment
    {
        public int Id { get; set; }
        public string commenttext { get; set; }

        public virtual User User { get; set; }
        public virtual post Post { get; set; }

    }
        
    public class friend_re
    {
        public int Id { get; set; }
        public int sender1_id { get; set; }

        public virtual User sender{ get; set;}

        public int resever1_id { get; set; }
        public virtual User resever{ get; set; }
    }
        
    public class friend
    {
        public int Id { get; set; }
        public int sender1_id { get; set; }
        public virtual User sender { get; set; }
        public int resever1_id { get; set; }
        public virtual User resever { get; set; }
    }

}