using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NAUB.Models;

namespace NAUB.Controllers.Api
{
    public class BooksController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/books
        public IHttpActionResult Get()
        {
            var books = _context.Books.ToList();
            if (books.Count < 1)
                return BadRequest("No books currently in stock");

            return Ok(books);
        }

        // GET api/books/5
        public IHttpActionResult Get(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        //TODO: POST api/books
        public void Post([FromBody]string value)
        {
        }

        //TODO: PUT api/books/5
        public IHttpActionResult Put(int id, Book book)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var bookInDb = _context.Books.SingleOrDefault(b => b.Id == book.Id);
            if (bookInDb == null)
                return NotFound();

            bookInDb.Isbn = book.Isbn;
            bookInDb.Title = book.Title;

            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/books/5
        public IHttpActionResult Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok();
        }
    }
}
