using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdminPortal.UI.Models.Models
{
    /// <summary>
    /// Model class to represent a grade 
    /// </summary>
    public class GradeModel
    {

        #region Properties

        /// <summary>
        /// GradeId
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(2)]
        [Display(Name ="Grade")]
        public string LetterGrade { get; set; }

        /// <summary>
        /// Property for the student that the grade was assigned to  
        /// </summary>
        public StudentModel Student { get; set; }

        /// <summary>
        /// Property for the course that the grade was assigned to 
        /// </summary>
        public CourseModel Course { get; set; }
        #endregion
    }
}
