using System.EnterpriseServices;
using NAUB.Models;
using NAUB.ViewModels;
using System.Linq;

namespace NAUB.Library
    {
        public class OverdueDetails
        {
            public bool IsOverdue { get; set; }
            public int OverdueAmount { get; set; }
        }
    public static class Overdue
        {
            public static OverdueDetails Details(ApplicationDbContext context,string borrowerId, string bookIsbn)
            {
                const int DEFAULT = 1;
                var settingsDetails = context.Settings.Single(s => s.Id == DEFAULT);
                var borrower = context.Borrows.Single(b => b.BorrowerId == borrowerId && b.Isbn == bookIsbn);

                var lendingDetails = new LendingViewModel
                {
                    Borrow = borrower, ExpectedReturnDate = borrower.BorrowDate.AddDays(settingsDetails.NumberOfDays)
                };

                var isOverdue = lendingDetails.IsOverdue();
                var overdueAmount = (lendingDetails.DaysRemaining() * -1) * settingsDetails.OverdueFeePerBook;

                return new OverdueDetails{IsOverdue = isOverdue, OverdueAmount = overdueAmount};
            }
        }
    }