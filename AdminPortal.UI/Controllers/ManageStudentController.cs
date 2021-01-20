using AdminPortal.UI.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AdminPortal.BL.BusinessClasses;

namespace AdminPortal.UI.Controllers
{
    /// <summary>
    /// Controller to manage students
    /// </summary>
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class ManageStudentController : Controller
    {

        #region Fields

        /// <summary>
        /// Field to the logger
        /// </summary>
        private readonly ILogger<ManageStudentController> _logger;

        /// <summary>
        /// Field to the student business object
        /// </summary>
        private readonly Student studentBusinessObject;

        #endregion

        #region Constructors



        /// <summary>
        /// Constructor that takes in a logger 
        /// </summary>
        /// <param name="logger">The logger to use</param>
        public ManageStudentController(ILogger<ManageStudentController> logger)
        {
            _logger = logger;
            studentBusinessObject = new Student();
        }

        #endregion


        /// <summary>
        /// Get action to the index page of manage students
        /// </summary>
        /// <returns>A view to the manage student page</returns>
        public async Task<IActionResult> Index()
        {
            return View("Views/Student/ManageStudent.cshtml",await studentBusinessObject.GetAllStudentAsync());
        }

        /// <summary>
        /// GET action to add or edit a student 
        /// </summary>
        /// <param name="id">the  id of the student for an add or edit</param>
        /// <returns>A view back to the manage student home page</returns>
        public async Task<IActionResult> AddEditStudent(int id = 0)
        {
            //add new student
            if (id == 0)
                return View("Views/Student/AddEditStudent.cshtml", new StudentModel());
            else
            {
                //edit
                var foundStudent = await studentBusinessObject.FindStudentAsync(id);
                return View("Views/Student/AddEditStudent.cshtml", foundStudent);
            }
        }

        /// <summary>
        /// Post action to add or edit a student
        /// </summary>
        /// <param name="student">The student to be added or edited</param>
        /// <returns>A view back to the mange student home page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditStudent([Bind("StudentId,FirstName,LastName")] StudentModel student)
        {

            if (ModelState.IsValid)
            {
                //add to the database
                if (student.StudentId == 0)
                    await studentBusinessObject.AddStudentAsync(student);

                //edit
                else
                    await studentBusinessObject.UpdateStudentAsync(student);

                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index), student);
        }

        /// <summary>
        /// Get action to delete a student from the database
        /// </summary>
        /// <param name="id">The id of the student to delete</param>
        /// <returns>A view back to the manage student page</returns>
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await studentBusinessObject.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
