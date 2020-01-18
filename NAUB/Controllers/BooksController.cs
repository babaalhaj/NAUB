using NAUB.Models;
using System.Linq;
using System.Web.Mvc;

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
		public JsonResult IsIsbnAvailable(string isbn, int id)
		{
			return Json(IsUnique(isbn, id), JsonRequestBehavior.AllowGet);
		}

		private bool IsUnique(string isbn, int id)
		{
			if (id != 0) // Its an existing object
				return !_context.Books.Any(b => b.Isbn == isbn && b.Id != id);
			
			// its a new object
			return !_context.Books.Any(b => b.Isbn == isbn);
		}

	}
}