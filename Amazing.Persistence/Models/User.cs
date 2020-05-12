using Amazing.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amazing.Persistence.Models
{
    public class User : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Not encrypted password due to test purpose
        /// </summary>
        public string Password { get; set; }

        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public DateTime CreationDate { get; set; }
    }
}