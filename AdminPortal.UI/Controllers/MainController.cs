﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPortal.UI.Controllers
{
    /// <summary>
    /// Controller For the Logged in administrator
    /// </summary>
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class MainController : Controller
    {

        #region Action Methods

        /// <summary>
        /// Action method for main
        /// </summary>
        /// <returns>the main view for the logged in user</returns>
        public IActionResult Index()
        {
            return View("Views/Main.cshtml");

        }

        #endregion
    }
}
