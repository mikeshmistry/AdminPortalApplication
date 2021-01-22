using AdminPortal.BL.BusinessClasses;
using AdminPortal.UI.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AdminPortal.UI.Controllers
{
    /// <summary>
    /// Class to manage teacher enrollment
    /// </summary>
    [Authorize]
    public class TeacherEnrollmentController : Controller
    {

        #region Fields

        /// <summary>
        /// Field to the logger
        /// </summary>
        private readonly ILogger<TeacherEnrollmentController> _logger;

        /// <summary>
        /// Field to the student business object
        /// </summary>
        private readonly Teacher teacherBusinessObject;

        /// <summary>
        /// Field to the student business object
        /// </summary>
        private readonly Course courseBusinessObject;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor that takes in a logger 
        /// </summary>
        /// <param name="logger">The logger to use</param>
        public TeacherEnrollmentController(ILogger<TeacherEnrollmentController> logger)
        {
            _logger = logger;
            teacherBusinessObject = new Teacher();
            courseBusinessObject = new Course();

        }

        #endregion

        #region Actions 

        /// <summary>
        /// Get action method to populate the drop down lists for teacher and courses
        /// </summary>
        /// <returns>A view to the student enrollment main page</returns>
        public async Task<IActionResult> Index()
        {
            var teacherEnrollmentModel = await PopulateTeacherAndCourseListAsync();
            return View("Views/Teacher/TeacherEnrollment.cshtml", teacherEnrollmentModel);
        }


        /// <summary>
        /// Post action method to enroll a student into a course
        /// </summary>
        /// <param name="teacher">The student to enroll into the course</param>
        /// <returns>A view to the student enrollment main page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollTeacher([Bind("TeacherId,CourseId")] TeacherEnrollmentModel teacher)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var error = new TeacherEnrollmentModel();
                error = await PopulateTeacherAndCourseListAsync();
                

                result = await courseBusinessObject.AssignTeacherToCourseAsync(teacher.TeacherId, teacher.CourseId);
                if (result == false)
                {

                    error.Error = true;
                }
                return View("Views/Teacher/TeacherEnrollment.cshtml", error);
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Non Action Methods

        /// <summary>
        /// No action method to populate the course and teacher list
        /// </summary>
        /// <returns>A TeacherEnrollmentModel</returns>
        [NonAction]
        private async Task<TeacherEnrollmentModel> PopulateTeacherAndCourseListAsync()
        {
            var teacherEnrollmentModel = new TeacherEnrollmentModel
            {
                Courses = await courseBusinessObject.GetAllCourseAsync(),
                Teachers = await teacherBusinessObject.GetAllTeacherAsync()
            };

            //Add the default value
            teacherEnrollmentModel.Teachers.Insert(0, new TeacherModel() { TeacherId = 0, FullName = "---Select A Teacher---" });

            //insert at the first the for selections
            teacherEnrollmentModel.Courses.Insert(0, new CourseModel() { CourseId = 0, Name = "---Select A Course---" });

            return teacherEnrollmentModel;
        }

        #endregion

    }
}
