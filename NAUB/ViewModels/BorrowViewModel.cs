using System.Collections.Generic;
using NAUB.Models;

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

        public Borrow Borrow { get; set; }
        public List<BorrowType> BorrowTypes { get; set; }
        public string[] MyBooks { get; set; }
    }
}