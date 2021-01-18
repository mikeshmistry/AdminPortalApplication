using AdminPortal.BL.BusinessClasses;
using AdminPortal.UI.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPortal.UI.Controllers
{
    /// <summary>
    /// Controller to manage courses
    /// </summary>
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class ManageCourseController : Controller
    {
        #region Fields

        /// <summary>
        /// Field to the logger 
        /// </summary>
        private readonly ILogger<ManageCourseController> _logger;

        /// <summary>
        /// Field to the course business object class
        /// </summary>
        private readonly Course courseBusinessObject;

        #endregion

        #region Constructors



        /// <summary>
        /// Constructor that takes in a logger 
        /// </summary>
        /// <param name="logger">the logger to be used</param>
        public ManageCourseController(ILogger<ManageCourseController> logger)
        {
            _logger = logger;
            courseBusinessObject = new Course();
        }

        #endregion


        /// <summary>
        /// Get action to the index page of manage courses
        /// </summary>
        /// <returns>A view to the manage course page</returns>
        public async Task<IActionResult> Index()
        {
            return View("Views/Course/ManageCourse.cshtml", await courseBusinessObject.GetAllCourseAsync());
        }

        /// <summary>
        /// Get action to add or edit a course 
        /// </summary>
        /// <param name="id">The id of the course for an add or edit</param>
        /// <returns>A view to the manage course page</returns>
        public async Task<IActionResult> AddEditCourse(int id = 0)
        {
            //add new course
            if (id == 0)
                return View("Views/Course/AddEditCourse.cshtml", new CourseModel());
            else
            {
                //edit
                var foundCourse = await courseBusinessObject.FindCourseAsync(id);
                return View("Views/Course/AddEditCourse.cshtml", foundCourse);
            }
        }

        /// <summary>
        /// Post action to perform the add or edit
        /// </summary>
        /// <param name="course">The course to be added or edited</param>
        /// <returns>A view back to the manage course home page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditCourse([Bind("CourseId,Name,Description")] CourseModel course)
        {
            if (ModelState.IsValid)
            {
                //add to the database
                if (course.CourseId == 0)
                    await courseBusinessObject.AddCourseAsync(course);

                //edit
                else
                    await courseBusinessObject.UpdateCourseAsync(course);

                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index), course);
        }

        /// <summary>
        /// Get action to delete a course 
        /// </summary>
        /// <param name="id">The id of the course to be deleted</param>
        /// <returns>A view back to manage course home page</returns>
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var student = await courseBusinessObject.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
