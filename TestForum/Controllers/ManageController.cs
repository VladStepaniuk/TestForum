using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TestForum.Models;

namespace TestForum.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        // GET: /Manage/Index
        [Authorize]
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            IList<string> roles = new List<string> { "Role is not defined." };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();

            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);

            var model = new IndexViewModel
            {
                Roles = (List<string>)roles,
                Username=userName
            };
            return View(model);
        }

    }
}