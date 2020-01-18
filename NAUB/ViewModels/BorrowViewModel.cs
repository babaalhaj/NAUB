using NAUB.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NAUB.ViewModels
{
    public class BorrowViewModel
    {
        public BorrowViewModel()
        {
            
        }
        public BorrowViewModel(short numberOfBooks)
        {
            MyBooks = new string[numberOfBooks];
        }
        [Required]
        public Borrow Borrow { get; set; }
        public List<BorrowType> BorrowTypes { get; set; }
        [Required]
        public string[] MyBooks { get; set; }
        public bool IsReturned { get; set; }
        }
}