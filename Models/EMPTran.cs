using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmpTask.Models
{
    public class EMPTran
    {
        public int RowId { get; set; }
        [Required]
        public int MasterId { get; set; }
        [Required]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Qty is required.")]
        public Decimal Qty { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        public Decimal Rate { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public Decimal Amount { get; set; }

    }
}