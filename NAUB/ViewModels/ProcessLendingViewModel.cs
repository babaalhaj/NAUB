using System.Collections.Generic;
using System.Linq;
using NAUB.Models;

namespace NAUB.ViewModels
{
    public class ProcessLendingViewModel
    {
        private readonly ApplicationDbContext _context;
        public ProcessLendingViewModel(ApplicationDbContext getContext)
        {
            _context = getContext;
        }

        public IEnumerable<LendingViewModel> GetLendingDetails()
        {
            var counter = 1;
            var borrowers = _context.Borrows.ToList();
            var borrowersList = new List<LendingViewModel>();
            var getDaysLimit = _context.Settings.FirstOrDefault();

            foreach (var borrower in borrowers)
            {
                if (getDaysLimit != null)
                    borrowersList.Add(new LendingViewModel
                    {
                        Borrow = borrower,
                        Id = counter,
                        ExpectedReturnDate = borrower.BorrowDate.AddDays(getDaysLimit.NumberOfDays)
                    });

                counter++;
            }

            return borrowersList;

        }
    }
}