using Amazing.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amazing.Persistence.Models
{
    public class Post : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public DateTime CreationDate { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
}