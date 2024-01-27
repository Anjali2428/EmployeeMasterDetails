using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmpTask.Models
{
    public class ProjectMaster
    {
        [Key]
        public int RowId { get; set; }

        [Required(ErrorMessage = "ProjectName is required.")]
        [StringLength(255, ErrorMessage = "ProjectName must be at most 255 characters.")]
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a non-negative value.")]
        public decimal Rate { get; set; }
    }
}