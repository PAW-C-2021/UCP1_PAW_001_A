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
    public class TransaksiDetailsController : Controller
    {
        private readonly TokoBukuContext _context;

        public TransaksiDetailsController(TokoBukuContext context)
        {
            _context = context;
        }

        // GET: TransaksiDetails
        public async Task<IActionResult> Index()
        {
            var tokoBukuContext = _context.TransaksiDetails.Include(t => t.KodeBukuNavigation).Include(t => t.KodeTransaksiNavigation);
            return View(await tokoBukuContext.ToListAsync());
        }

        // GET: TransaksiDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaksiDetail = await _context.TransaksiDetails
                .Include(t => t.KodeBukuNavigation)
                .Include(t => t.KodeTransaksiNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaksiDetail == id);
            if (transaksiDetail == null)
            {
                return NotFound();
            }

            return View(transaksiDetail);
        }

        // GET: TransaksiDetails/Create
        public IActionResult Create()
        {
            ViewData["KodeBuku"] = new SelectList(_context.Bukus, "KodeBuku", "KodeBuku");
            ViewData["KodeTransaksi"] = new SelectList(_context.Transaksis, "KodeTransaksi", "KodeTransaksi");
            return View();
        }

        // POST: TransaksiDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransaksiDetail,KodeTransaksi,KodeBuku")] TransaksiDetail transaksiDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaksiDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KodeBuku"] = new SelectList(_context.Bukus, "KodeBuku", "KodeBuku", transaksiDetail.KodeBuku);
            ViewData["KodeTransaksi"] = new SelectList(_context.Transaksis, "KodeTransaksi", "KodeTransaksi", transaksiDetail.KodeTransaksi);
            return View(transaksiDetail);
        }

        // GET: TransaksiDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaksiDetail = await _context.TransaksiDetails.FindAsync(id);
            if (transaksiDetail == null)
            {
                return NotFound();
            }
            ViewData["KodeBuku"] = new SelectList(_context.Bukus, "KodeBuku", "KodeBuku", transaksiDetail.KodeBuku);
            ViewData["KodeTransaksi"] = new SelectList(_context.Transaksis, "KodeTransaksi", "KodeTransaksi", transaksiDetail.KodeTransaksi);
            return View(transaksiDetail);
        }

        // POST: TransaksiDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransaksiDetail,KodeTransaksi,KodeBuku")] TransaksiDetail transaksiDetail)
        {
            if (id != transaksiDetail.IdTransaksiDetail)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaksiDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaksiDetailExists(transaksiDetail.IdTransaksiDetail))
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
            ViewData["KodeBuku"] = new SelectList(_context.Bukus, "KodeBuku", "KodeBuku", transaksiDetail.KodeBuku);
            ViewData["KodeTransaksi"] = new SelectList(_context.Transaksis, "KodeTransaksi", "KodeTransaksi", transaksiDetail.KodeTransaksi);
            return View(transaksiDetail);
        }

        // GET: TransaksiDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaksiDetail = await _context.TransaksiDetails
                .Include(t => t.KodeBukuNavigation)
                .Include(t => t.KodeTransaksiNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaksiDetail == id);
            if (transaksiDetail == null)
            {
                return NotFound();
            }

            return View(transaksiDetail);
        }

        // POST: TransaksiDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaksiDetail = await _context.TransaksiDetails.FindAsync(id);
            _context.TransaksiDetails.Remove(transaksiDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaksiDetailExists(int id)
        {
            return _context.TransaksiDetails.Any(e => e.IdTransaksiDetail == id);
        }
    }
}
