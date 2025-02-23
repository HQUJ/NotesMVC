using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotesMVC.Data;
using NotesMVC.Models;

namespace NotesMVC.Controllers
{
    //promqna - it requires roles
    [Authorize]
    public class NotesController : Controller
    {
        //promqna
        private readonly NotesMVCContext _context;
        private readonly UserManager<Client> _userManager;

        //promqna
        public NotesController(NotesMVCContext context, UserManager<Client> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Notes
        //promqna - this is a searcher that searches by string and returns the notes that contain the string in their title or their description
        public async Task<IActionResult> Index(string searchTerm)
        {
            //promqna  
            var notes = from n in _context.Note select n; // Get all notes

            if (!string.IsNullOrEmpty(searchTerm))
            {
                notes = notes.Where(n => n.Title.Contains(searchTerm) || n.Description.Contains(searchTerm));
            }

            ViewData["SearchTerm"] = searchTerm;
            //if the user is an admin they will be able to see all notes
            if (User.IsInRole("admin"))
                return View(await notes.ToListAsync());
            //if the user is not an admin they will be able to see anly their notes
            return View(await notes.Where(x => x.ClientEmail == User.Identity.Name).ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        
        //promqna
        //[HttpPost]
        //[ValidateInput(false)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //promqna
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Note note)
        {
            if (ModelState.IsValid)
            {
                //promnqa
                note.LastUpdateOn = DateTime.Now;
                var currentUser = await _userManager.GetUserAsync(User);
                note.ClientEmail = User.Identity.Name;
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //promqna
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ClientEmail")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //promqna
                    note.LastUpdateOn = DateTime.Now;
                    //if (User.Identity.Name != "admin@admin.com")
                    //    note.ClientEmail = User.Identity.Name;
                    //else
                    //    note.ClientEmail = email;
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.FindAsync(id);
            if (note != null)
            {
                _context.Note.Remove(note);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}
