using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NAUB.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "# of books is required!!!")]
        [Display(Name = "Maximum number of books")]
        public short MaximumNumberOfBooksPerBorrow { get; set; }

        [Required(ErrorMessage = "# of days is required!!!")]
        [Display(Name = "Number of days")]
        public short NumberOfDays { get; set; }

        [Required(ErrorMessage = "Amount is required!!!")]
       [Display(Name = "Overdue fee per book")]
        public short OverdueFeePerBook { get; set; }
    }
}