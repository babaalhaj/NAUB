using System;
using System.ComponentModel.DataAnnotations;

namespace NAUB.Models
    {
    public class Borrow
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Borrower's Identity is required!!!"), StringLength(10), 
         Display(Name = "Borrower's Identity")]
        public string BorrowerType { get; set; }

        [Required(ErrorMessage = "Borrower's Id number is required"), StringLength(50), 
         Display(Name = "Borrower's Id number")]
        public string BorrowerId { get; set; }

        [Required(ErrorMessage = "Borrower's name is required"), Display(Name="Borrower's full name"),
        StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "ISBN is required"), StringLength(50), Display(Name = "ISBN #")]
        public string Isbn { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}