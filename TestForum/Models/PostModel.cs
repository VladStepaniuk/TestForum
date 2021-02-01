using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestForum.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage ="Content must be at least 5 symbols.")]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TopicId { get; set; }
        public TopicModel Topic { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }

    public class PostNewModel
    {
        public string TopicName { get; set; }
        public int TopicId { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 symbols.")]
        public string Content { get; set; }
    }
}