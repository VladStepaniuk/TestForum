using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestForum.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        [MinLength(10, ErrorMessage = "Title must be at least 10 characters!")]
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<PostModel> Posts { get; set; }
    }

    public class TopicViewCreate
    {
        [Required]
        [Display(Name = "Topic Name")]
        [MinLength(10, ErrorMessage ="Title must be at least 10 characters!")]
        public string Title { get; set; }

    }

    public class TopicIndexView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name ="Author")]
        public string AuthorName { get; set; }
        [Display(Name ="Date of create")]
        public DateTime CreatedDate { get; set; }
        [Display(Name ="Number of posts")]
        public int PostCount { get; set; }
    }

}