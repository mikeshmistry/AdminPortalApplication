using AdminPortal.UI.Models.SecurityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AdminPortal.UI.Controllers
{
    /// <summary>
    /// Controller for the home page for non logged in users
    /// </summary>
    public class HomeController : Controller
    {
        #region Fields

        private readonly ILogger<HomeController> _logger;

        #endregion

        #region Constructors

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Action method for Home
        /// </summary>
        /// <returns>The default view for home</returns>
        public IActionResult Index()
        {
            return View();
        }

        

        /// <summary>
        /// Action method for errors
        /// </summary>
        /// <returns>A error with the error view model</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
