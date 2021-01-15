using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPortal.UI.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class ManageTeacherController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Teacher/ManageTeacher.cshtml");
        }
    }
}
