﻿using NAUB.Library;
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
            ViewBag.Borrowers = _context.Borrows.Count();
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
            // TODO: Implement save borrow validation
            //if (!ModelState.IsValid)
            //    return View("AddRecord", borrowViewModel);

            Booking.AddBooking(borrowViewModel, _context);

            var processBorrowers = new ProcessLendingViewModel(_context);

            return View("Index", processBorrowers.GetLendingDetails());
            }



        // GET:/Borrows/Extend
        public ActionResult Extend(string borrowerId, string bookIsbn)
            {
            var borrow = _context.Borrows.SingleOrDefault(b => b.BorrowerId == borrowerId && b.Isbn == bookIsbn);

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
            ViewBag.Borrowers = _context.Borrows.Count();
            var processBorrowers = new ProcessLendingViewModel(_context);

            return View("Index", processBorrowers.GetLendingDetails());
            }

        // GET:/Borrows/ReturnBook
        public ActionResult ReturnBook(string borrowerId, string bookIsbn)
            {
                // Get borrowers details
                var borrow = _context.Borrows.Single(b => b.BorrowerId == borrowerId && b.Isbn == bookIsbn);

                // Set the return date and flag the book as returned.
                borrow.ReturnDate = DateTime.Now;
                borrow.IsReturned = true;

                var book = _context.Books.Single(b => b.Isbn == bookIsbn);

                // Add the number of books in stock.
                book.NumberInStock++;

                _context.SaveChanges();
            
            // Initialize the Lending List
            ViewBag.Borrowers = _context.Borrows.Count();
            var processBorrowers = new ProcessLendingViewModel(_context);
            return View("Index", processBorrowers.GetLendingDetails());
            }
        }
    }