using Amazing.Persistence.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Amazing.Persistence.Models
{
    public class Content : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int Sort { get; set; }
        public DateTime CreationDate { get; set; }
    }
}