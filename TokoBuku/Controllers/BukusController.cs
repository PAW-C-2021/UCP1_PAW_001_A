using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TokoBuku.Models;

namespace TokoBuku.Controllers
{
    public class BukusController : Controller
    {
        private readonly TokoBukuContext _context;

        public BukusController(TokoBukuContext context)
        {
            _context = context;
        }

        // GET: Bukus
        public async Task<IActionResult> Index()
        {
            var tokoBukuContext = _context.Bukus.Include(b => b.CreatedByNavigation);
            return View(await tokoBukuContext.ToListAsync());
        }

        // GET: Bukus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Bukus
                .Include(b => b.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.KodeBuku == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }

        // GET: Bukus/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Users, "IdUser", "Email");
            return View();
        }

        // POST: Bukus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KodeBuku,Judul,Penerbit,Deskripsi,Harga,CreatedBy")] Buku buku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "IdUser", "Email", buku.CreatedBy);
            return View(buku);
        }

        // GET: Bukus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Bukus.FindAsync(id);
            if (buku == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "IdUser", "Email", buku.CreatedBy);
            return View(buku);
        }

        // POST: Bukus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("KodeBuku,Judul,Penerbit,Deskripsi,Harga,CreatedBy")] Buku buku)
        {
            if (id != buku.KodeBuku)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BukuExists(buku.KodeBuku))
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
            ViewData["CreatedBy"] = new SelectList(_context.Users, "IdUser", "Email", buku.CreatedBy);
            return View(buku);
        }

        // GET: Bukus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Bukus
                .Include(b => b.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.KodeBuku == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }

        // POST: Bukus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var buku = await _context.Bukus.FindAsync(id);
            _context.Bukus.Remove(buku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BukuExists(string id)
        {
            return _context.Bukus.Any(e => e.KodeBuku == id);
        }
    }
}
