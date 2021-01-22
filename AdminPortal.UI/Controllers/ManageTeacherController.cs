using AdminPortal.BL.BusinessClasses;
using AdminPortal.UI.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AdminPortal.UI.Controllers
{
    /// <summary>
    /// Controller to manage teachers
    /// </summary>
    [Authorize]
    public class ManageTeacherController : Controller
    {
        #region Fields

        /// <summary>
        /// Field to the logger 
        /// </summary>
        private readonly ILogger<ManageTeacherController> _logger;

        /// <summary>
        /// Field to the teacher business object class
        /// </summary>
        private readonly Teacher teacherBusinessObject;

        #endregion

        #region Constructors



        /// <summary>
        /// Constructor that takes in a logger 
        /// </summary>
        /// <param name="logger">the logger to be used</param>
        public ManageTeacherController(ILogger<ManageTeacherController> logger)
        {
            _logger = logger;
            teacherBusinessObject = new Teacher();
        }

        #endregion


        /// <summary>
        /// Get action to the index page of manage teachers
        /// </summary>
        /// <returns>A view to the manage teacher page</returns>
        public async Task<IActionResult> Index()
        {
            return View("Views/Teacher/ManageTeacher.cshtml", await teacherBusinessObject.GetAllTeacherAsync());
        }

        /// <summary>
        /// Get action to add or edit a teacher 
        /// </summary>
        /// <param name="id">The id of the teacher for add or edit</param>
        /// <returns>A view to the manage teacher page</returns>
        public async Task<IActionResult> AddEditTeacher(int id = 0)
        {
            //add new teacher
            if (id == 0)
                return View("Views/Teacher/AddEditTeacher.cshtml", new TeacherModel());
            else
            {
                //edit
                var foundTeacher = await teacherBusinessObject.FindTeacherAsync(id);
                return View("Views/Teacher/AddEditTeacher.cshtml", foundTeacher);
            }
        }

        /// <summary>
        /// Post action to perform the add or edit
        /// </summary>
        /// <param name="teacher">The teacher to be added or edited</param>
        /// <returns>A view back to the manage teacher home page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditTeacher([Bind("TeacherId,FirstName,LastName")] TeacherModel teacher)
        {
            if (ModelState.IsValid)
            {
                //add to the database
                if (teacher.TeacherId == 0)
                    await teacherBusinessObject.AddTeacherAsync(teacher);

                //edit
                else
                    await teacherBusinessObject.UpdateTeacherAsync(teacher);

                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index), teacher);
        }

        /// <summary>
        /// Get action to delete a teacher 
        /// </summary>
        /// <param name="id">The id of the teacher to be deleted</param>
        /// <returns>A view back to manage teacher home page</returns>
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var student = await teacherBusinessObject.DeleteTeacherAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
