using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private LibraryContext context { get; set; }

        public BookController(LibraryContext c)
        {
            context = c;
        }

        [Route("[Controller]s")]
        public ViewResult List()
        {
            List<Book> books = context.Books.Include(b => b.Author).Include(b => b.Genre).ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            BookEditViewModel bookEdit = new BookEditViewModel();
            bookEdit.Authors = context.Authors.OrderBy(a => a.FirstName).ToList();
            bookEdit.Genres = context.Genres.OrderBy(g => g.Name).ToList();
            bookEdit.Series = context.Series.OrderBy(g => g.Name).ToList();

            if (id > 0)
            {
                bookEdit.Book = context.Books.Find(id);
                ViewBag.Title = "Edit";
                return View(bookEdit);
            }

            else
            {
                bookEdit.Book = new Book();
                ViewBag.Title = "Add";
                return View(bookEdit);
            }
        }
        [HttpPost]
        public IActionResult Edit(BookEditViewModel bookEdit)
        {
            if (!ModelState.IsValid)
            {
                bookEdit.Authors = context.Authors.OrderBy(a => a.FirstName).ToList();
                bookEdit.Genres = context.Genres.OrderBy(g => g.Name).ToList();
                bookEdit.Series = context.Series.OrderBy(g => g.Name).ToList();
                ViewBag.Title = bookEdit.Book.BookId == 0 ? "Add" : "Edit";

                return View(bookEdit);
            }

            if (bookEdit.Book.BookId == 0)
            {
                context.Books.Add(bookEdit.Book);
            }
            else
            {
                context.Books.Update(bookEdit.Book);
            }
            context.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Book book = context.Books.Include(b => b.Author).Include(b => b.Genre).Include(b => b.Series).Where(b => b.BookId == id).FirstOrDefault();
            return View(book);
        }
        
        public IActionResult Favorite(int id)
        {
            Book book = context.Books.Find(id);
            string changed = "";
            if(book.Favorite == true)
            {
                book.Favorite = false;
                changed = "removed from";
            }

            else
            {
                book.Favorite = true;
                changed = "added to";
            }
            
            context.Update(book);
            context.SaveChanges();

            TempData["ActionMessage"] = book.Title + " has been " + changed + " favorites.";
            return RedirectToAction("Favorites");
        }

        [HttpGet]
        public IActionResult Favorites()
        {
            List<Book> books = context.Books.Include(b => b.Author).
                Include(b => b.Genre).Where(b => b.Favorite == true).ToList();

            return View(books);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Book book = context.Books.Find(id);

            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            
            context.Books.Remove(book);
            context.SaveChanges();
            TempData["ActionMessage"] = book.Title + " has been deleted";
            return RedirectToAction("List");
        }
    }
}
