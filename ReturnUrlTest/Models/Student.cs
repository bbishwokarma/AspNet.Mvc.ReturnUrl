using System;
using System.ComponentModel.DataAnnotations;

namespace ReturnUrlTest.Models
{
    public class Student
    {
        [Key]
        public string StudentId { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; }
    }
}