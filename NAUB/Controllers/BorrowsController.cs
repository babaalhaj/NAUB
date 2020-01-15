using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NAUB.Models;
using NAUB.ViewModels;

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
            if(disposing)
                _context.Dispose();
        }

        // TODO: List of Borrowers
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
                               +" books");

            
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

            var books = _context.Books.Where(b => borrowViewModel.MyBooks.Contains(b.Isbn)).ToList();

            short counter = 0;
            foreach (var book in books)
            {
                book.NumberInStock--;

                var borrows = new Borrow
                {
                    BorrowerType = borrowViewModel.Borrow.BorrowerType,
                    BorrowerId = borrowViewModel.Borrow.BorrowerId,
                    Name = borrowViewModel.Borrow.Name,
                    Isbn = borrowViewModel.MyBooks[counter],
                    BorrowDate = DateTime.Today
                };

                counter++;
                _context.Borrows.Add(borrows);
            }

            _context.SaveChanges();

            return RedirectToAction("Index","Home");
        }
	}
}