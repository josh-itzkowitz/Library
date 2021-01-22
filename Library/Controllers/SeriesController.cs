using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class SeriesController : Controller
    {
        private LibraryContext context { get; set; }
        public SeriesController(LibraryContext c) => context = c;

        public ViewResult List()
        {
            List<Series> series = context.Series.Include(s => s.Author).Include(s => s.Genre).ToList();
            return View(series);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SeriesEditViewModel seriesEdit = new SeriesEditViewModel();
            seriesEdit.Authors = context.Authors.ToList();
            seriesEdit.Genres = context.Genres.ToList();

            if (id > 0)
            {
                seriesEdit.Series = context.Series.Find(id);
                ViewBag.Title = "Edit";
                return View(seriesEdit);
            }

            else
            {
                seriesEdit.Series = new Series();
                ViewBag.Title = "Add";
                return View(seriesEdit);
            }
        }
        [HttpPost]
        public IActionResult Edit(SeriesEditViewModel seriesEdit)
        {
            if (!ModelState.IsValid)
            {
                seriesEdit.Authors = context.Authors.ToList();
                seriesEdit.Genres = context.Genres.ToList();
                ViewBag.Title = seriesEdit.Series.SeriesId == 0 ? "Add" : "Edit";

                return View(seriesEdit);
            }

            if (seriesEdit.Series.SeriesId == 0)
            {
                context.Series.Add(seriesEdit.Series);
            }
            else
            {
                context.Series.Update(seriesEdit.Series);
            }
            context.SaveChanges();

            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Series series = context.Series.Include(s => s.Author).Include(s => s.Genre).Where(s => s.SeriesId == id).FirstOrDefault();
            series.Books = context.Books.Where(b => b.SeriesId == id).OrderBy(b => b.SeriesNum).ToList();

            return View(series);
        }

        [HttpGet]
        public IActionResult AddBook(int id)
        {
            AddToSeriesViewModel addToSeriesViewModel = new AddToSeriesViewModel();
            addToSeriesViewModel.SrsId = id;
            addToSeriesViewModel.Name = context.Series.Find(id).Name;
            addToSeriesViewModel.AuthName = context.Series.Include(s => s.Author).Where(s => s.SeriesId == id).FirstOrDefault().Author.Name;
            int authId = context.Series.Where(s => s.SeriesId == id).FirstOrDefault().AuthorId;
            addToSeriesViewModel.Books = context.Books.Where(b => b.AuthorId == authId).Where(b => b.SeriesId == null).ToDictionary(b => b.BookId.ToString(), b => b.Title);
            return View(addToSeriesViewModel);
        }
        [HttpPost]
        public IActionResult AddBook(AddToSeriesViewModel addToSeriesViewModel)
        {
            Book book = context.Books.Find(addToSeriesViewModel.BkId);
            book.SeriesId = addToSeriesViewModel.SrsId;
            book.SeriesNum = addToSeriesViewModel.SrsNum;
            context.Update(book);
            context.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Series series = context.Series.Find(id);

            return View(series);
        }

        [HttpPost]
        public IActionResult Delete(Series series)
        {
            List<Book> books = context.Books.Where(b => b.SeriesId == series.SeriesId).ToList();
            if(books.Count == 0)
            {
                context.Series.Remove(series);
                context.SaveChanges();
                TempData["ActionMessage"] = series.Name + " has been deleted";
                return RedirectToAction("List");
            }
            else
            {
                series.Books = context.Books.Where(b => b.SeriesId == series.SeriesId).ToList();
                return View("DeleteError", series);
            }
        }

        public IActionResult DeleteBooks(int id)
        {
            Series series = context.Series.Find(id);

            List<Book> books = context.Books.Where(b => b.SeriesId == id).ToList();

            foreach (Book item in books)
            {
                context.Books.Remove(item);
            }

            context.Series.Remove(series);

            context.SaveChanges();
            TempData["ActionMessage"] = series.Name + " and all its books were deleted";

            return RedirectToAction("List");
        }
    }
}