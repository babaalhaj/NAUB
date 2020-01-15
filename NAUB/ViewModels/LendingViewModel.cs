using System;
using NAUB.Models;

namespace NAUB.ViewModels
{
    public class LendingViewModel
    {
        public int Id { get; set; }
        public Borrow Borrow { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string BorrowType
        {
            get { return Borrow.BorrowerType == "1" ? "Staff" : "Student"; }
        }
    }
}