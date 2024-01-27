using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EmpTask.Models
{
    public class employeeWiseProMaster
    {
        [Key]
        public int RowId { get; set; }

        [Required(ErrorMessage = "ProjectNo is required.")]
        public int ProjectNo { get; set; }

        [Required(ErrorMessage = "AssignDate is required.")]
        [DataType(DataType.Date)]
        public DateTime AssignDate { get; set; }

        [Required(ErrorMessage = "EmployeeId is required.")]
        [DisplayName("Employee ID")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Rate/TotalAmount is required.")]
        public decimal TotalAmount { get; set; }

        [Range(0, 100, ErrorMessage = "DiscPerc must be between 0 and 100.")]
        public decimal? DiscPerc { get; set; }

        public decimal? DiscAmount { get; set; }

        [Required(ErrorMessage = "NetAmount is required.")]
        public decimal NetAmount { get; set; }


       
    }
}