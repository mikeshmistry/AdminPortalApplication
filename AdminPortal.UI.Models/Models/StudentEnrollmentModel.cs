using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace AdminPortal.UI.Models.Models
{
    /// <summary>
    /// Model class to represent enrolling a student into a course
    /// </summary>
    public class StudentEnrollmentModel
    {
     
        #region Properties 

        /// <summary>
        /// Selected Id for the student
        /// </summary>
        [Range(1,Int32.MaxValue, ErrorMessage ="You Must Select A Student")]
        public int StudentId { get; set; }
        
        

        /// <summary>
        /// Selected Id for the course
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "You Must Select A Course")]
        public int CourseId { get; set; }


        /// <summary>
        /// List of all the students
        /// </summary>
        [Display(Name ="Select A Student :")]
        public List<StudentModel> Students { get; set; }

        /// <summary>
        /// List of all courses
        /// </summary>
        [Display(Name ="Select A Course:")]
        public List<CourseModel> Courses { get;  set; }

        #endregion

    }
}
