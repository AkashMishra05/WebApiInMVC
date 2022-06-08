using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcEmployeeModel
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Name Field is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Position Field is Required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Age Field is Required")]
        public Nullable<int> Age { get; set; }

        [Required(ErrorMessage = "Salary Field is Required")]
        public Nullable<int> Salary { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifedOn { get; set; }
    }
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
    }
}