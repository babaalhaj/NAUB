using NAUB.Models;
using System;

namespace NAUB.ViewModels
    {
    public class LendingViewModel
        {
        public int Id { get; set; }
        public Borrow Borrow { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string BorrowType => Borrow.BorrowerType == "1" ? "Staff" : "Student";
        public int DefaultDaysRemaining() => (int)(ExpectedReturnDate.Date - Borrow.BorrowDate.Date).TotalDays;
        public int DaysRemaining() => (int)(ExpectedReturnDate.Date - DateTime.Now.Date).TotalDays ;


        }
    }