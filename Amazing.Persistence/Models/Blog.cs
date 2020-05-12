using Amazing.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amazing.Persistence.Models
{
    public class Blog : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Url { get; set; }
        public ICollection<Post> Posts { get; set; }
        public DateTime CreationDate { get; set; }
    }
}