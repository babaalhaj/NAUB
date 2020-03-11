using NAUB.Library;
using NAUB.Models;
using NAUB.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace NAUB.Controllers
{
    public class BorrowsController : Controller
        {
        private readonly ApplicationDbContext _context;

        public BorrowsController()
            {
            _context = new ApplicationDbContext();
            }

        protected override void Dispose(bool disposing)
            {
            if (disposing)
                _context.Dispose();
            }

        // GET: /Borrows/
        public ActionResult Index()
            {
            ViewBag.Borrowers = _context.Borrows.Where(b => b.IsReturned == false).ToList().Count();
            var processBorrowers = new ProcessLendingViewModel(_context);
            return View(processBorrowers.GetLendingDetails());
            }

        //
        public ActionResult AddRecord(short getCount)
            {
            var getSettings = _context.Settings.FirstOrDefault();

            if (getSettings != null && getCount > getSettings.MaximumNumberOfBooksPerBorrow)
                return Content("The maximum number of books exceeded. The maximum per individual is "
                               + getSettings.MaximumNumberOfBooksPerBorrow
                               + " books");


            var borrowViewModel = new BorrowViewModel(getCount)
                {
                BorrowTypes = _context.BorrowTypes.ToList()
                };

            return View(borrowViewModel);
            }

        public ActionResult NumberOfBooks()
            {
            return View();
            }

        // TODO: GET: /Borrows/SaveRecord
        [HttpPost]
        public ActionResult AddRecord(BorrowViewModel borrowViewModel)
            {

            // Check if borrower has books in his/her possession.
            var borrowersId = borrowViewModel.Borrow.BorrowerId;
            var isbn = borrowViewModel.Borrow.Isbn;
            var checkBook = _context.Borrows
                .SingleOrDefault(b => b.IsReturned == false && b.BorrowerId == borrowersId && b.Isbn == isbn);
            
            if (checkBook != null)
            {
                var model = new BorrowViewModel((byte)borrowViewModel.MyBooks.Length)
                {
                    BorrowTypes = _context.BorrowTypes.ToList()
                };

                var message =
                    $@"Borrower has {isbn} book in his/her possession already.";
                ModelState.AddModelError(string.Empty, message);
                return View("AddRecord", model);
            }

            var validator = _context.Borrows
                .Where(b => b.BorrowerId == borrowViewModel.Borrow.BorrowerId && b.IsReturned == false);
            var settings = _context.Settings.Single(s => s.Id == 1);

            if (validator.Count() >= settings.MaximumNumberOfBooksPerBorrow)
            {
                var model = new BorrowViewModel((byte)borrowViewModel.MyBooks.Length)
                {
                    BorrowTypes = _context.BorrowTypes.ToList()
                };

                var message =
                    string.Format(
                        @"Maximum number of books exceeded, the borrower has {0} books in his/her possession already.",
                        validator.Count());
                ModelState.AddModelError(string.Empty, message);
                return View("AddRecord", model);
            }


            Booking.AddBooking(borrowViewModel, _context);

            var processBorrowers = new ProcessLendingViewModel(_context);

            ViewBag.Borrowers = _context.Borrows.Where(b => b.IsReturned == false).ToList().Count();

            return View("Index", processBorrowers.GetLendingDetails());
            }



        // GET:/Borrows/Extend
        public ActionResult Extend(string borrowerId, string bookIsbn)
            {
            var borrow = _context.Borrows.Single(b => b.BorrowerId == borrowerId && b.Isbn == bookIsbn);

            var extend = new ExtendViewModel
                {
                BorrowerId = borrow.BorrowerId,
                BorrowerType = borrow.BorrowerType,
                Name = borrow.Name,
                NumberOfDays = 0,
                Isbn = borrow.Isbn
                };

            return View(extend);
            }



        [HttpPost]
        public ActionResult Extend(ExtendViewModel model)
            {
            // Arrange
            var borrow = _context.Borrows.Single(b => b.BorrowerId == model.BorrowerId && b.Isbn == model.Isbn);
            borrow.BorrowDate = borrow.BorrowDate.AddDays(model.NumberOfDays);

            // Save extension date to database
            _context.SaveChanges();

            // Initialize the Lending List
            ViewBag.Borrowers = _context.Borrows.Where(b => b.IsReturned == false).ToList().Count();
            var processBorrowers = new ProcessLendingViewModel(_context);

            return View("Index", processBorrowers.GetLendingDetails());
            }

        
        public ActionResult ReturnBook(string borrowerId, string bookIsbn)
        {

            // Check and see if overdue
            var checkOverdue = Overdue.Details(_context, borrowerId, bookIsbn);
            // Get borrowers details
            var borrow = _context.Borrows.Single(b => b.BorrowerId == borrowerId && b.Isbn == bookIsbn);


            if (checkOverdue.IsOverdue)
            {
                var overdue = new OverdueViewModel { Borrow = borrow, OverdueDetails = checkOverdue };
                return View("ReturnBook", overdue);
            }

            // Set the return date and flag the book as returned.
            borrow.ReturnDate = DateTime.Now;
            borrow.IsReturned = true;

            var book = _context.Books.Single(b => b.Isbn == bookIsbn);

            // Add the number of books in stock.
            book.NumberInStock++;

            _context.SaveChanges();

            // Initialize the Lending List
            var processBorrowers = new ProcessLendingViewModel(_context);
            ViewBag.Borrowers = _context.Borrows.Where(b => b.IsReturned == false).ToList().Count();
            return View("Index", processBorrowers.GetLendingDetails());
        }

        [HttpPost]
        public ActionResult OverdueReturnBook(OverdueViewModel model)
        {
            var processBorrowers = new ProcessLendingViewModel(_context);
            

            if (model.AlreadyPaid)
            {
                var book = _context.Books.Single(b => b.Isbn == model.Borrow.Isbn);
                var borrow = _context.Borrows.Single(b => b.Id == model.Borrow.Id);

                // Set the return date and flag the book as returned.
                borrow.ReturnDate = DateTime.Now;
                borrow.IsReturned = true;

                // Add the number of books in stock.
                book.NumberInStock++;

                _context.SaveChanges();

                // Initialize the Lending List
                ViewBag.Borrowers = _context.Borrows.Where(b => b.IsReturned == false).ToList().Count();

                return View("Index", processBorrowers.GetLendingDetails());
            }

            // Initialize the Lending List
            ViewBag.Borrowers = _context.Borrows.Where(b => b.IsReturned == false).ToList().Count();
            return View("Index", processBorrowers.GetLendingDetails());
        }

    }
    }