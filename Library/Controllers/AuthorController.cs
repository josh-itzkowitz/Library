using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private LibraryContext context { get; set; }

        public AuthorController(LibraryContext c) => context = c;

        [Route("[Controller]s")]
        public ViewResult List()
        {
            List<Author> authors = context.Authors.ToList();

            return View(authors);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //Author already has an ID, so we are editing
            if (id > 0)
            {
                Author author = context.Authors.Find(id);
                ViewBag.Title = "Edit";
                return View(author);
            }

            else
            {
                Author author = new Author();
                ViewBag.Title = "Add";
                return View(author);
            }
        }
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = author.AuthorId == 0 ? "Add" : "Edit";
                               
                return View(author);
            }

            if (author.AuthorId == 0)
            {
                context.Authors.Add(author);
            }
            else
            {
                context.Authors.Update(author);
            }
            context.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Author author = context.Authors.Include(a => a.Books).Where(a => a.AuthorId == id).FirstOrDefault();
            author.Books = context.Books.Where(b => b.AuthorId == id).Include(b => b.Genre).ToList();

            return View(author);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Author author = context.Authors.Find(id);

            return View(author);
        }

        [HttpPost]
        public IActionResult Delete(Author author)
        {
            List<Book> books = context.Books.Where(b => b.AuthorId == author.AuthorId).ToList();
            if (books.Count == 0)
            {
                context.Authors.Remove(author);
                context.SaveChanges();
                TempData["ActionMessage"] = author.FirstName + " " + author.LastName + " has been deleted";
                return RedirectToAction("List");
            }
            else
            {
                author.Books = context.Books.Where(b => b.AuthorId == author.AuthorId).ToList();
                return View("DeleteError", author);
            }
        }
        
        public IActionResult DeleteBooks(int id)
        {
            Author author = context.Authors.Find(id);

            List<Book> books = context.Books.Where(b => b.AuthorId == id).ToList();

            foreach(Book item in books)
            {
                context.Books.Remove(item);
            }

            context.Authors.Remove(author);
            
            context.SaveChanges();
            TempData["ActionMessage"] = author.Name + " and all their books were deleted";

            return RedirectToAction("List"); 
        }

        public IActionResult Favorite(int id)
        {
            Author author = context.Authors.Find(id);
            string changed = "";
            if (author.Favorite == true)
            {
                author.Favorite = false;
                changed = "removed from";
            }

            else
            {
                author.Favorite = true;
                changed = "added to";
            }

            context.Update(author);
            context.SaveChanges();

            TempData["ActionMessage"] = author.Name + " has been " + changed + " favorites.";
            return RedirectToAction("Favorites");
        }

        [HttpGet]
        public IActionResult Favorites()
        {
            List<Author> authors = context.Authors.Where(b => b.Favorite == true).ToList();

            return View(authors);
        }
    }
}