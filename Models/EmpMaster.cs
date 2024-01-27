using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmpTask.Models
{
    public class EmpMaster
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]

        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid mobile number. Please enter a 10-digit number.")]
        public string Mobile { get; set; }


        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(5000, 20000, ErrorMessage = "Salary must be between 5000 and 20000")]
        public decimal Salary { get; set; }

        [NotMapped]
        public string EmployeeName
        {
            get { return FirstName + " " + LastName; }
        }





    }
}