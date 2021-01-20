using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPortal.UI.Models.Models
{
    /// <summary>
    /// Model class for assigning a course to a teacher
    /// </summary>
    public class TeacherEnrollmentModel : ModelBase
    {
        /// <summary>
        /// Selected TeacherId
        /// </summary>
        [Range(1,Int32.MaxValue,ErrorMessage ="You Must Select A Teacher")]
        public int TeacherId { get; set; }

        /// <summary>
        /// Selected CourseId
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "You Must Select A Course")]
        public int CourseId { get; set; }

        /// <summary>
        /// List of all teachers
        /// </summary>
        [Display(Name = "Select A Teacher:")]
        public List<TeacherModel> Teachers { get; set; }

        /// <summary>
        /// List of all courses
        /// </summary>
        [Display(Name ="Select A Course:")]
        public List<CourseModel> Courses { get; set; }
    }
}
