using ReturnUrlTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReturnUrlTest.ViewModels
{
    public class StudentsViewModel
    {
        [Display(Name = "Search Name (First or Last)")]
        public string SearchName { get; set; }

        public List<Student> Students { get; set; }
    }
}