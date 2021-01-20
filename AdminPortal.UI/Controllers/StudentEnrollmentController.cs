using AdminPortal.BL.BusinessClasses;
using AdminPortal.UI.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AdminPortal.UI.Controllers
{
    /// <summary>
    /// Class to manage student enrollment
    /// </summary>
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class StudentEnrollmentController : Controller
    {

        #region Fields

        /// <summary>
        /// Field to the logger
        /// </summary>
        private readonly ILogger<StudentEnrollmentController> _logger;

        /// <summary>
        /// Field to the student business object
        /// </summary>
        private readonly Student studentBusinessObject;

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
        public StudentEnrollmentController(ILogger<StudentEnrollmentController> logger)
        {
            _logger = logger;
            studentBusinessObject = new Student();
            courseBusinessObject = new Course();

        }
        #endregion


        #region Actions 

        /// <summary>
        /// Get action method to populate the drop down lists for courses and students
        /// </summary>
        /// <returns>A view to the student enrollment main page</returns>
        public async Task<IActionResult> Index()
        {
            var studentEnrollmentModel = await PopulateStudentAndCourseListAsync();
            return View("Views/Student/StudentEnrollment.cshtml", studentEnrollmentModel);
        }


        /// <summary>
        /// Post action method to enroll a student into a course
        /// </summary>
        /// <param name="student">The student to enroll into the course</param>
        /// <returns>A view to the student enrollment main page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollStudent([Bind("StudentId,CourseId")] StudentEnrollmentModel student)
        {
            var test = false;

            if (ModelState.IsValid)
                test = await courseBusinessObject.EnrollStudentInCourseAysnc(student.StudentId, student.CourseId);
            
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Non Action Methods

        /// <summary>
        /// No action method to populate the course and student list
        /// </summary>
        /// <returns>A StudentEnrollmentModel</returns>
        [NonAction]
        private async Task<StudentEnrollmentModel> PopulateStudentAndCourseListAsync()
        {
            var studentEnrollmentModel = new StudentEnrollmentModel
            {
                Courses = await courseBusinessObject.GetAllCourseAsync(),
                Students = await studentBusinessObject.GetAllStudentAsync()
            };

            //Add the default value
            studentEnrollmentModel.Students.Insert(0, new StudentModel() { StudentId = 0, FullName = "---Select A Student---" });

            //insert at the first the for selections
            studentEnrollmentModel.Courses.Insert(0, new CourseModel() { CourseId = 0, Name = "---Select A Course---" });

            return studentEnrollmentModel;
        }

        #endregion 
    }
}
