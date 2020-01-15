using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using NAUB.Models;

namespace NAUB.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                _context.Dispose();
        }

        //
        // GET: /Books/
        public ActionResult Index()
        {
            ViewBag.Catalog = _context.Books.ToList().Count;
            return View();
        }

        // GET: /Books/Create
        public ViewResult Create()
        {
            var newBook = new Book();
            return View("BookForm", newBook);
        }

        // GET: /Books/Edit/1
        public ViewResult Edit(int id)
        {
            var bookInDb = _context.Books.SingleOrDefault(b => b.Id == id);
            if (bookInDb == null) return View("Index");


            return View("UpdateBookForm", bookInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult SaveBook(Book book)
        {
            if (!ModelState.IsValid)
                return View("BookForm");

            if (book.Id == 0)
             _context.Books.Add(book);
            else
            {
                var bookInDb = _context.Books.SingleOrDefault(b => b.Id == book.Id);
                TryUpdateModel(bookInDb);
            }
            
            _context.SaveChanges();

            ViewBag.Catalog = _context.Books.ToList().Count;
            return View("Index");

        }
        
        [HttpPost]
        public JsonResult IsIsbnAvailable(string Isbn, int Id)
        {
            return Json(IsUnique(Isbn, Id), JsonRequestBehavior.AllowGet);
        }

        private bool IsUnique(string Isbn, int Id)
        {
            if (Id != 0) // Its an existing object
                return !_context.Books.Any(b => b.Isbn == Isbn && b.Id != Id);
            
            // its a new object
            return !_context.Books.Any(b => b.Isbn == Isbn);
        }

	}
}