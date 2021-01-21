using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AdminPortal.UI.Controllers
{
    /// <summary>
    /// Controller For the Logged in administrator
    /// </summary>
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class MainController : Controller
    {
        #region Fields

        private readonly ILogger<MainController> _logger;
       

        #endregion

        #region Constructors

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        #endregion 

        #region Action Methods

        /// <summary>
        /// Get action method for main
        /// </summary>
        /// <returns>The main view for the logged in user</returns>
        public IActionResult Index()
        {
            return RedirectToAction("Index", "ManageStudent");
           
        }

        #endregion
    }
}
