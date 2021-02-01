using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace TestForum.Models
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}